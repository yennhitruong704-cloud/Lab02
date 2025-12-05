using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Lab02_04
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CalculateTotalBalance();
        }

        // Hàm tìm kiếm ListViewItem theo Số Tài Khoản
        private ListViewItem FindItemByAccountID(string accountID)
        {
            foreach (ListViewItem item in listViewAccounts.Items)
            {
                if (item.Text.Equals(accountID, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            return null;
        }

        // Hàm tính toán và hiển thị tổng số tiền trong tất cả tài khoản
        private void CalculateTotalBalance()
        {
            long total = 0;
            foreach (ListViewItem item in listViewAccounts.Items)
            {
                if (long.TryParse(item.SubItems[3].Text, out long balance)) // SubItems[3] là cột Số tiền
                {
                    total += balance;
                }
            }
            lblTotal.Text = $"Tổng tiền: {total.ToString("N0")} VNĐ";
        }

        // Hàm gán/cập nhật dữ liệu từ controls vào ListViewItem
        private void UpdateListViewItem(ListViewItem item, string name, string address, long balance)
        {
            item.SubItems[1].Text = name;
            item.SubItems[2].Text = address;
            item.SubItems[3].Text = balance.ToString(); // Lưu dạng số để tính toán
        }

        // Yêu cầu 4.1: Thêm / Cập Nhật
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra thông tin bắt buộc
                if (string.IsNullOrWhiteSpace(txtAccountID.Text) || string.IsNullOrWhiteSpace(txtCustomerName.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text) || string.IsNullOrWhiteSpace(txtBalance.Text))
                {
                    throw new Exception("Vui lòng nhập đầy đủ thông tin!");
                }

                // Kiểm tra Số tiền có hợp lệ không
                if (!long.TryParse(txtBalance.Text, out long balance) || balance < 0)
                {
                    throw new Exception("Số tiền trong tài khoản không hợp lệ.");
                }

                string accountID = txtAccountID.Text;
                ListViewItem existingItem = FindItemByAccountID(accountID);

                if (existingItem == null)
                {
                    // TH Thêm mới
                    ListViewItem newItem = new ListViewItem(accountID);
                    newItem.SubItems.Add(txtCustomerName.Text);
                    newItem.SubItems.Add(txtAddress.Text);
                    newItem.SubItems.Add(balance.ToString());

                    listViewAccounts.Items.Add(newItem);
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // TH Cập nhật
                    UpdateListViewItem(existingItem, txtCustomerName.Text, txtAddress.Text, balance);
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                CalculateTotalBalance();
                ClearInputFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Yêu cầu 4.2: Xóa
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAccountID.Text))
                {
                    MessageBox.Show("Vui lòng nhập Số tài khoản cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ListViewItem itemToDelete = FindItemByAccountID(txtAccountID.Text);

                if (itemToDelete == null)
                {
                    throw new Exception("Không tìm thấy số tài khoản cần xóa!");
                }

                // Xuất cảnh báo YES/NO
                DialogResult dr = MessageBox.Show($"Bạn có muốn xóa tài khoản {itemToDelete.Text} không?", "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dr == DialogResult.Yes)
                {
                    listViewAccounts.Items.Remove(itemToDelete);
                    MessageBox.Show("Xóa tài khoản thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CalculateTotalBalance();
                    ClearInputFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Yêu cầu 4.3: Xử lý sự kiện chọn 1 dòng trong ListView
        private void listViewAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Chỉ xử lý khi có ít nhất một mục được chọn
            if (listViewAccounts.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewAccounts.SelectedItems[0];

                // Gán ngược dữ liệu ra các controls nhập liệu
                txtAccountID.Text = selectedItem.SubItems[0].Text;
                txtCustomerName.Text = selectedItem.SubItems[1].Text;
                txtAddress.Text = selectedItem.SubItems[2].Text;
                txtBalance.Text = selectedItem.SubItems[3].Text;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Hàm dọn dẹp các trường nhập liệu
        private void ClearInputFields()
        {
            txtAccountID.Clear();
            txtCustomerName.Clear();
            txtAddress.Clear();
            txtBalance.Clear();
            txtAccountID.Focus();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
