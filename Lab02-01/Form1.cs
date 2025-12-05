using System;
using System.Windows.Forms;

namespace Lab02-01
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Hàm hỗ trợ thực hiện phép toán và quản lý lỗi
        private void Calculate(string operation)
        {
            try
            {
                // Kiểm tra xem các ô nhập liệu có bị trống không
                if (string.IsNullOrWhiteSpace(txtNumber1.Text) || string.IsNullOrWhiteSpace(txtNumber2.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ Số thứ nhất và Số thứ hai.", "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Chuyển đổi dữ liệu nhập vào sang kiểu float
                float number1 = float.Parse(txtNumber1.Text);
                float number2 = float.Parse(txtNumber2.Text);
                float result = 0;

                // Thực hiện phép toán
                switch (operation)
                {
                    case "+":
                        result = number1 + number2;
                        break;
                    case "-":
                        result = number1 - number2;
                        break;
                    case "*":
                        result = number1 * number2;
                        break;
                    case "/":
                        // Kiểm tra chia cho 0
                        if (number2 == 0)
                        {
                            throw new DivideByZeroException("Không thể chia cho 0.");
                        }
                        result = number1 / number2;
                        break;
                }

                // Hiển thị kết quả
                txtAnswer.Text = result.ToString();
            }
            // Bắt lỗi khi người dùng nhập dữ liệu không phải là số (ví dụ: chữ)
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu nhập vào không hợp lệ. Vui lòng nhập số.", "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Bắt lỗi chia cho 0
            catch (DivideByZeroException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Toán Học", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            // Bắt các lỗi khác (lỗi chung)
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện Click cho nút Cộng
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Calculate("+");
        }

        // Sự kiện Click cho nút Trừ
        private void btnSub_Click(object sender, EventArgs e)
        {
            Calculate("-");
        }

        // Sự kiện Click cho nút Nhân
        private void btnMul_Click(object sender, EventArgs e)
        {
            Calculate("*");
        }

        // Sự kiện Click cho nút Chia
        private void btnDiv_Click(object sender, EventArgs e)
        {
            Calculate("/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Thiết lập ô kết quả chỉ đọc
            txtAnswer.ReadOnly = true;
        }
    }
}