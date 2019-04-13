// Program 2
// CIS 200-01
// Fall 2018
// Due: 11/12/2018
// By: D7184

// File: Prog2Form.cs
// This class creates the main GUI for Program 2. It provides a
// File menu with About, Open, Save As and Exit items, an Insert menu with Address and
// Letter items, a Report menu with List Addresses and List Parcels
// items, and an edit menu with addresses.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using Prog2;

namespace UPVApp
{
    [Serializable] 
    public partial class Prog2Form : Form
    {
        private UserParcelView upv; // The UserParcelView
        private BinaryFormatter formatter = new BinaryFormatter();  //The Binary Formatter
        private BinaryFormatter reader = new BinaryFormatter(); //The Binary reader
        private FileStream input; //Filestream for the input 
        private FileStream output; //Filestream for the output



        // Precondition:  None
        // Postcondition: The form's GUI is prepared for display. A few test addresses are
        //                added to the list of addresses
        public Prog2Form()
        {
            InitializeComponent();

            upv = new UserParcelView();

        }

        // Precondition:  File, About menu item activated
        // Postcondition: Information about author displayed in dialog box
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string NL = Environment.NewLine; // Newline shorthand

            MessageBox.Show($"Program 3{NL}Due: 11/12/2018{NL}By: D7184{NL}CIS 200{NL}Fall 2018",
                "About Program 3");
        }

        // Precondition:  File, Exit menu item activated
        // Postcondition: The application is exited
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Precondition:  Insert, Address menu item activated
        // Postcondition: The Address dialog box is displayed. If data entered
        //                are OK, an Address is created and added to the list
        //                of addresses
        private void addressToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddressForm addressForm = new AddressForm();    // The address dialog box form
            DialogResult result = addressForm.ShowDialog(); // Show form as dialog and store result
            int zip; // Address zip code

            if (result == DialogResult.OK) // Only add if OK
            {
                if (int.TryParse(addressForm.ZipText, out zip))
                {
                    upv.AddAddress(addressForm.AddressName, addressForm.Address1,
                        addressForm.Address2, addressForm.City, addressForm.State,
                        zip); // Use form's properties to create address
                }
                else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Address Validation!", "Validation Error");
                }
            }

            addressForm.Dispose(); // Best practice for dialog boxes
                                   // Alternatively, use with using clause as in Ch. 17
        }

        // Precondition:  Report, List Addresses menu item activated
        // Postcondition: The list of addresses is displayed in the addressResultsTxt
        //                text box
        private void listAddressesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Addresses:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Address a in upv.AddressList)
            {
                result.Append(a.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
            }

            reportTxt.Text = result.ToString();


            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  Insert, Letter menu item activated
        // Postcondition: The Letter dialog box is displayed. If data entered
        //                are OK, a Letter is created and added to the list
        //                of parcels
        private void letterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LetterForm letterForm; // The letter dialog box form
            DialogResult result;   // The result of showing form as dialog
            decimal fixedCost;     // The letter's cost

            if (upv.AddressCount < LetterForm.MIN_ADDRESSES) // Make sure we have enough addresses
            {
                MessageBox.Show("Need " + LetterForm.MIN_ADDRESSES + " addresses to create letter!",
                    "Addresses Error");
                return; // Exit now since can't create valid letter
            }

            letterForm = new LetterForm(upv.AddressList); // Send list of addresses
            result = letterForm.ShowDialog();

            if (result == DialogResult.OK) // Only add if OK
            {
                if (decimal.TryParse(letterForm.FixedCostText, out fixedCost))
                {
                    // For this to work, LetterForm's combo boxes need to be in same
                    // order as upv's AddressList
                    upv.AddLetter(upv.AddressAt(letterForm.OriginAddressIndex),
                        upv.AddressAt(letterForm.DestinationAddressIndex),
                        fixedCost); // Letter to be inserted
                }
               else // This should never happen if form validation works!
                {
                    MessageBox.Show("Problem with Letter Validation!", "Validation Error");
                }
            }

            letterForm.Dispose(); // Best practice for dialog boxes
                                  // Alternatively, use with using clause as in Ch. 17
        }

        // Precondition:  Report, List Parcels menu item activated
        // Postcondition: The list of parcels is displayed in the parcelResultsTxt
        //                text box
        private void listParcelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringBuilder result = new StringBuilder(); // Holds text as report being built
                                                        // StringBuilder more efficient than String
            decimal totalCost = 0;                      // Running total of parcel shipping costs
            string NL = Environment.NewLine;            // Newline shorthand

            result.Append("Parcels:");
            result.Append(NL); // Remember, \n doesn't always work in GUIs
            result.Append(NL);

            foreach (Parcel p in upv.ParcelList)
            {
                result.Append(p.ToString());
                result.Append(NL);
                result.Append("------------------------------");
                result.Append(NL);
                totalCost += p.CalcCost();
            }

            result.Append(NL);
            result.Append($"Total Cost: {totalCost:C}");

            reportTxt.Text = result.ToString();

            // Put cursor at start of report
            reportTxt.Focus();
            reportTxt.SelectionStart = 0;
            reportTxt.SelectionLength = 0;
        }

        // Precondition:  File, Save As menu item activated
        // Postcondition: Serailizes the address and saves them to a file
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result; //Holds the Dialog result
            string fileName; //Holds the file name

            using (SaveFileDialog fileChooser = new SaveFileDialog())
            {
                fileChooser.CheckFileExists = false; //Don't check if the file exsists

                result = fileChooser.ShowDialog(); //Dialog result equals te result of the file chooser dialog
                fileName = fileChooser.FileName; //File name 
            }

            if (result == DialogResult.OK) //user clicked ok
            {
                if (string.IsNullOrEmpty(fileName)) 
                {
                    MessageBox.Show("Invalid file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //Error if user doesn;t enter a file name 
                }
                else
                {
                    try
                    {
                        output = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write); //save file with file stream

                        formatter.Serialize(output, upv.AddressList);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("Error reading from file", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //Show error if the fiel is corrupted 
                    }

                }
            }
        }


        // Precondition:  File, Open menu item activated
        // Postcondition: DeSerailizes the addresses and saves them to the addresseslist
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result; //Holds the Dialog result
            string fileName;  //Holds the file name

            using (OpenFileDialog fileChooser = new OpenFileDialog())
            {
                result = fileChooser.ShowDialog(); //Dialog result equlas reslut of the file chooser
                fileName = fileChooser.FileName;   // finle name equlas the file name from the file chooser
            }

            if (result == DialogResult.OK) //user clicked ok
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    MessageBox.Show("Invalid file name", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);  //error if the iser doesn't enter a file name
                }
                else
                {
                    input = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read); //open file with filestream
                    
                    upv.addresses = (List<Address>)reader.Deserialize(input);//store the contents of the file in to the addresses list

                    
                }
            }

        }

        // Precondition:  Edit, Address menu item activated
        // Postcondition: Dialog to seect address is shown, if user selects address
        // and clicks OK address dialog is shown to edit address info
        private void addressToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            EditAddress editAddress = new EditAddress(upv.addresses); //edit addrss form passes addresses
            DialogResult result = editAddress.ShowDialog();//stores the result of the editaddress dialog

            if (result == DialogResult.OK) //if the user clicks ok
            {
                Address address = upv.AddressAt(editAddress.AddressIndex); //stores the address the user selected to be edited

                AddressForm addressForm = new AddressForm();  //new instance of the address form

                //Populate the address text boxes with the info to be edited
                addressForm.AddressName = address.Name;
                addressForm.Address1 = address.Address1;
                addressForm.Address2 = address.Address2;
                addressForm.City = address.City;
                addressForm.State = address.State;
                addressForm.ZipText = address.Zip.ToString();

                DialogResult editresult = addressForm.ShowDialog();  //stores the result of the address dialog

                if (editresult == DialogResult.OK) //if user clicked ok
                {
                    //reassigns the addresses properties to the contents of the address forms fields
                    address.Name = addressForm.AddressName;
                    address.Address1 = addressForm.Address1;
                    address.Address2 = addressForm.Address2;
                    address.City = addressForm.City;
                    address.State = addressForm.State;
                    address.Zip = int.Parse(addressForm.ZipText);
                }

                addressForm.Dispose(); //dispose of the address form
            }
            editAddress.Dispose(); //dispose of the edit address form
        }
    }
}