using System.Collections.Generic;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System;
using System.Text;

public partial class _Default : System.Web.UI.Page
{
    private readonly string CheckBoxId = "detailCheckBox";

    /// <summary>
    /// This HashSet stores the rows which are expanded
    /// </summary>
    private HashSet<string> ExpandedRows
    {
        get
        {
            object obj = Session["ExpandedRows"];
            if (obj == null)
            {
                Session["ExpandedRows"] = new HashSet<string>();
            }
            return (HashSet<string>)Session["ExpandedRows"];
        }
        set
        {
            Session["ExpandedRows"] = value;
        }
    }

    /// <summary>
    /// The dictionary is used to store the selected items for each TableView.
    /// </summary>
    private Dictionary<string, HashSet<string>> TableViews
    {
        get
        {
            object obj = Session["Dictionary"];
            if (obj == null)
            {
                Session["Dictionary"] = new Dictionary<string, HashSet<string>>();
            }
            return (Dictionary<string, HashSet<string>>)Session["Dictionary"];
        }
        set
        {
            Session["Dictionary"] = value;
        }
    }

    /// <summary>
    /// Naming of the template in the child GridTableViews that holds the checkbox
    /// </summary>
    private readonly string CheckBoxTemplateColumnUniqueName = "CheckBoxTemplate";

    protected void Page_Init(object sender, EventArgs e)
    {
        if (TableViews.Count == 0)
        {
            //On initial load create items in the dictionary that correspond to the GridTableViewNames
            Dictionary<string, HashSet<string>> dict = new Dictionary<string, HashSet<string>>();
            GridTableView tableView = RadGrid1.MasterTableView;
            while (tableView.DetailTables.Count != 0)
            {
                dict.Add(tableView.Name, new HashSet<string>());
                if (tableView.DetailTables.Count > 0)
                {
                    tableView = tableView.DetailTables[0];
                }
            }
            dict.Add(tableView.Name, new HashSet<string>());
            TableViews = dict;
        }

    }

    protected void MasterCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox box = (CheckBox)sender;
        GridDataItem dataItem = (GridDataItem)box.NamingContainer;
        //Extracts the masterKeyName and adds it or removes it from the collection in the dictionary
        string masterKeyName = GenerateUniqueIdentifier(dataItem);
        this.SetCollectionValue(TableViews[dataItem.OwnerTableView.Name], masterKeyName, !TableViews[dataItem.OwnerTableView.Name].Contains(masterKeyName));

        //Saves the expanded state and if the item is not expanded expands it in order to traverse the records
        bool expanded = dataItem.Expanded;
        dataItem.Expanded = true;
        dataItem.Selected = box.Checked;

        GridTableView nestedTableView = dataItem.ChildItem.NestedTableViews[0];
        //Selects or deselects all the child items
        CheckItemsInTableView(nestedTableView, box.Checked);
        //Returns the previous state
        dataItem.Expanded = expanded;
    }

    /// <summary>
    /// According to the ckeck state of the master checkbox selects or deselects the items down in the hierarchy
    /// </summary>
    /// <param name="tableView"></param>
    /// <param name="shouldCheck"></param>
    protected void CheckItemsInTableView(GridTableView tableView, bool shouldCheck)
    {
        int currentPageSize = tableView.PageSize;
        int currentPageIndex = tableView.CurrentPageIndex;
        //Resizing the table view in order to increase the performance
        tableView.PageSize = int.MaxValue;
        tableView.Rebind();
        foreach (GridDataItem detailItem in tableView.Items)
        {
            detailItem.Selected = shouldCheck;
            (detailItem[CheckBoxTemplateColumnUniqueName].FindControl(CheckBoxId) as CheckBox).Checked = shouldCheck;
            GridTableView ownerTableView = detailItem.OwnerTableView;
            //Generates a unique identifier for the data item. This is done in order to distinguish every data item in the grid
            string dataKeyValue = GenerateUniqueIdentifier(detailItem);
            //Select or deselect the items in the current TableView
            this.SetCollectionValue(TableViews[ownerTableView.Name], dataKeyValue, shouldCheck);

            detailItem.Expanded = true;
            //If there is next level of hierarchy exist select or deselectes the items in it
            if (detailItem.ChildItem != null)
            {
                CheckItemsInTableView(detailItem.ChildItem.NestedTableViews[0], shouldCheck);
            }
        }
        tableView.PageSize = currentPageSize;
        tableView.CurrentPageIndex = currentPageIndex;
        tableView.Rebind();
    }

    /// <summary>
    /// Checking if all the items in the GridTableView are checked in order to determine whether to check the master item
    /// </summary>
    /// <param name="nestedTableView"></param>
    /// <returns></returns>
    protected bool ShouldCheckMasterItem(GridTableView nestedTableView)
    {
        bool isChecked = true;
        int currentPageSize = nestedTableView.PageSize;
        int currentPageIndex = nestedTableView.CurrentPageIndex;
        //Resizing the table view in order to increase the performance
        nestedTableView.PageSize = int.MaxValue;
        nestedTableView.Rebind();
        foreach (GridDataItem detailItem in nestedTableView.Items)
        {
            string currentDataItem = GenerateUniqueIdentifier(detailItem);
            if (!TableViews[nestedTableView.Name].Contains(currentDataItem))
            {
                isChecked = false;
                break;
            }
        }
        nestedTableView.PageSize = currentPageSize;
        nestedTableView.CurrentPageIndex = currentPageIndex;
        nestedTableView.Rebind();
        return isChecked;
    }
    
    /// <summary>
    /// Handles the check of the an item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DetailCheckbox_CheckedChanged(object sender, EventArgs e)
    {
        //Check or uncheck the item
        CheckBox box = (CheckBox)sender;
        GridDataItem dataItem = (GridDataItem)box.NamingContainer;
        dataItem.Selected = box.Checked;

        bool shouldCheckOwner = true;

        //Gets a unique value for the grid item
        string uniqueIdentifier = GenerateUniqueIdentifier(dataItem);

        this.SetCollectionValue(TableViews[dataItem.OwnerTableView.Name], uniqueIdentifier, box.Checked);
        //If the current data items has child items check or unchecks all child items
        if (dataItem.HasChildItems)
        {
            bool dataItemExpandedState = dataItem.Expanded;
            dataItem.Expanded = true;
            CheckItemsInTableView(dataItem.ChildItem.NestedTableViews[0], box.Checked);
            dataItem.Expanded = dataItemExpandedState;
        }
        //Moving up in the hierarchy and determining whether to check the master items
        while (dataItem.OwnerTableView.ParentItem!=null)
        {
            GridTableView ownerTableView = dataItem.OwnerTableView;
            //Checking if the owner should be selected 
            shouldCheckOwner = ShouldCheckMasterItem(ownerTableView);

            GridDataItem parentItem = ownerTableView.ParentItem;
            string ownerTableName = parentItem.OwnerTableView.Name;
        
            //Extracting the unique signature of the parent item
            string currentOwner = GenerateUniqueIdentifier(ownerTableView.ParentItem);

            //Selecteing or deselecting the owner
            this.SetCollectionValue(TableViews[ownerTableName], currentOwner, shouldCheckOwner);
            (parentItem[CheckBoxTemplateColumnUniqueName].FindControl(CheckBoxId) as CheckBox).Checked = shouldCheckOwner;
            parentItem.Selected = shouldCheckOwner;

            //Getting a reference to a data item in an upper level of the grid
            dataItem = parentItem;
        }
    }

    protected string GenerateUniqueIdentifier(GridDataItem item)
    {
        StringBuilder uniqueIdentifier = new StringBuilder();
        GridDataItem currentItem = item;
        //Traversing the parent items up in the hierarchy to generate a unique key
        while (true)
        {
            uniqueIdentifier.Append(currentItem.GetDataKeyValue(currentItem.OwnerTableView.DataKeyNames[0]).ToString());
            currentItem = currentItem.OwnerTableView.ParentItem;
            if (currentItem == null)
            {
                break;
            }
        }
        return uniqueIdentifier.ToString();
    }
    //Selects the items down in the hierarchy
    private void SelectChildTableItems(GridDataItem dataItem)
    {
        dataItem.Expanded = ExpandedRows.Contains(dataItem.GetDataKeyValue(dataItem.OwnerTableView.DataKeyNames[0]).ToString());
        //If the item is expanded selects the child items if it is not the expanded the items should not be selected
        if (dataItem.Expanded)
        {
            GridTableView nestedTableView = dataItem.ChildItem.NestedTableViews[0];
            this.SelectItems(TableViews[nestedTableView.Name], nestedTableView.Items, null);
        }
    }

    protected void RadGrid1_PreRender(object sender, EventArgs e)
    {
        this.SelectItems(TableViews[RadGrid1.MasterTableView.Name], RadGrid1.MasterTableView.Items, this.SelectChildTableItems);
    }

    /// <summary>
    /// Selects the items which have been previously selected
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="items"></param>
    /// <param name="callback"></param>
    private void SelectItems(HashSet<string> collection, GridDataItemCollection items, Action<GridDataItem> callback)
    {
        GridDataItem currentDataItem;
        for (int i = 0; i < items.Count; i++)
        {
            currentDataItem = items[i];
            //Generates the unique identifier for the item
            string uniqueIdentifier = GenerateUniqueIdentifier(currentDataItem);

            //Checks if the item was previously selected
            if (collection.Contains(uniqueIdentifier))
            {
                (currentDataItem.FindControl(CheckBoxId) as CheckBox).Checked = true;
                currentDataItem.Selected = true;
            }
            if (callback != null)
            {
                callback.Invoke(currentDataItem);
            }
            if (currentDataItem.ChildItem != null)
            {
                // If the item has a nested table view selects the child items
                bool expanded = currentDataItem.Expanded;
                currentDataItem.Expanded = true;
                GridTableView ownerTableView = currentDataItem.ChildItem.NestedTableViews[0];
                string ownerTableViewDataKeyName = ownerTableView.DataKeyNames[0];
                this.SelectItems(TableViews[ownerTableView.Name], ownerTableView.Items, callback);
                //Returns the previous expanded state
                currentDataItem.Expanded = expanded;
            }
        }
    }

    private void SetCollectionValue(HashSet<string> collection, string value, bool selected)
    {
        if (selected)
        {
            collection.Add(value);
        }
        else
        {
            collection.Remove(value);
        }
    }

    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "ExpandCollapse")
        {
            //Saves the item unique valiue in the expanded rows collecting in order to persist the expanding
            GridDataItem item = e.Item as GridDataItem;
            string masterKeyName = item.GetDataKeyValue(item.OwnerTableView.DataKeyNames[0]).ToString();
            this.SetCollectionValue(ExpandedRows, masterKeyName, !e.Item.Expanded);
        }
    }
}