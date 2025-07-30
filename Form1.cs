/*
Design a Winforms personal phone/address book using a Dictionary collection.

Write a Person class to store details of a person like:
    FirstName
    LastName
    Mobile Phone
    Work Phone
    Address

Provide a grid to display all, add more, delete (with confirmation message), and search by name.

Search should show details of person searched. Use the name of the person as the key.
*/

namespace Assignment_4._1._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            personBindingSource.DataSource = Person.personsBindingList;

            // Begin Mock Data
            Person.personsDict.Add("billy windows", new Person() { FirstName = "Billy", LastName = "Windows", MobilePhone = "123-456-7890", WorkPhone = "456-789-0123", Address = "1 Microsoft Way, Redmond, WA 98052" });
            Person.personsDict.Add("tim apple", new Person() { FirstName = "Tim", LastName = "Apple", MobilePhone = "555-666-7890", WorkPhone = "223-456-7451", Address = "1 Apple Park Way, Cupertino, CA 95014" });
            Person.personsDict.Add("linus linux", new Person() { FirstName = "Linus", LastName = "Linux", MobilePhone = "987-813-1255", WorkPhone = "554-654-0001", Address = "1316 SW Corbett Hill Cir, Portland, OR 97219" });

            foreach (Person person in Person.personsDict.Values)
            {
                Person.personsBindingList.Add(person);
            }
            // End Mock Data
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (IsNameEmpty())
            {
                MissingNameError();
                return;
            }

            if (Search())
                Display(Person.personsDict.GetValueOrDefault(Key()));
            else
                MessageBox.Show("Contact does not exist!", "No result", MessageBoxButtons.OK);
        }

        private void SelectRow(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            
            Display(Person.personsBindingList[dataGridView1.SelectedRows[0].Index]);
        }

        private void Display(Person person)
        {
            txtFirstName.Text = person.FirstName;
            txtLastName.Text = person.LastName;
            txtMobilePhone.Text = person.MobilePhone;
            txtWorkPhone.Text = person.WorkPhone;
            txtAddress.Text = person.Address;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFirstName.Text = String.Empty;
            txtLastName.Text = String.Empty;
            txtMobilePhone.Text = String.Empty;
            txtWorkPhone.Text = String.Empty;
            txtAddress.Text = String.Empty;
            dataGridView1.ClearSelection();
        }

        private void ChangeAddButton(object sender, EventArgs e)
        {
            if (Search())
                btnAdd.Text = "Update Contact";
            else
                btnAdd.Text = "Add Contact";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (IsNameEmpty())
            {
                MissingNameError();
                return;
            }

            if (!Search())
            {
                var newPerson = new Person()
                {
                    FirstName = txtFirstName.Text,
                    LastName = txtLastName.Text,
                    MobilePhone = txtMobilePhone.Text,
                    WorkPhone = txtWorkPhone.Text,
                    Address = txtAddress.Text
                };

                Person.personsDict.Add((txtFirstName.Text + " " + txtLastName.Text).ToLower(), newPerson);
                Person.personsBindingList.Add(newPerson);
                btnAdd.Text = "Update Contact";
            }
            else
            {
                DialogResult messageBox = MessageBox.Show("Are you sure you want to update the contact with this new information?", "Update Confirmation", MessageBoxButtons.YesNo);

                if (messageBox == DialogResult.Yes)
                {
                    Person person = Person.personsDict.GetValueOrDefault(Key());
                    person.MobilePhone = txtMobilePhone.Text;
                    person.WorkPhone = txtWorkPhone.Text;
                    person.Address = txtAddress.Text;
                    personBindingSource.ResetBindings(false);
                }
            }
        }

        private bool IsNameEmpty()
        {
            return (txtFirstName.Text == "" || txtLastName.Text == "");
        }

        private void MissingNameError()
        {
            MessageBox.Show("Please enter both a First and Last name to proceed.", "Error", MessageBoxButtons.OK);
        }

        public bool Search()
        {
            return Person.personsDict.ContainsKey(Key());
        }

        public string Key()
        {
            return (txtFirstName.Text + " " + txtLastName.Text).Trim().ToLower();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (IsNameEmpty())
            {
                MissingNameError();
                return;
            }

            if (Search())
            {
                DialogResult messageBox = MessageBox.Show("Are you sure you want to delete this contact?", "Delete Confirmation", MessageBoxButtons.YesNo);
                if (messageBox == DialogResult.Yes)
                {
                    string key = Key();

                    Person.personsBindingList.Remove(Person.personsDict.GetValueOrDefault(key));
                    Person.personsDict.Remove(key);
                }
            }
            else
                MessageBox.Show("Contact does not exist!", "Error", MessageBoxButtons.OK);
        }
    }
}