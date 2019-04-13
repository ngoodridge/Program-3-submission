// Program 2
// CIS 200-01
// Fall 2018
// Due: 11/12/2018
// By: D7184

// File: EditAddress.cs
// This class creates the GUI for the user to selectt and address to edit
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Prog2
{
    public partial class EditAddress : Form
    {
        const int NO_SELECTION = -1; //Constant used for validation to make sure user selects an address
        public EditAddress(List<Address> addresses)
        {
            InitializeComponent();

            foreach(Address a in addresses)
            {
                AddressCombo.Items.Add(a.Name); //populate the combo box
            }
        }

        private void AddressCombo_validating(object sender, CancelEventArgs e)
        {
            if (AddressCombo.SelectedIndex == NO_SELECTION)
            {
                e.Cancel = true;
                errorProvider1.SetError(AddressCombo, "Please seleect and address"); //Error if the user doesn't make a selection
            }
        }

        private void Edit_OK_Click(object sender, EventArgs e) //if the user clicks ok
        {
            if (ValidateChildren()) //make sure the children were validate
                DialogResult = DialogResult.OK; //set dialog result to ok
        }

        private void AddressCombo_validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(AddressCombo, ""); //clear the error povider if the user selects an address
        }

        // Precondition:  User slected an address from the combo box
        // Postcondition: the index of the selection is stored
        internal int AddressIndex
        {

            get
            {
                return AddressCombo.SelectedIndex; //returns the index of the combo selection
            }

            set
            {
                if (value >= -1) //make sure they made a selection
                    AddressCombo.SelectedIndex = value;
                else
                    throw new ArgumentOutOfRangeException("AddressIndex", value,
                        "Please select and Address"); //if they didn't make a selectionn throw an error
            }
        }
    }
}
