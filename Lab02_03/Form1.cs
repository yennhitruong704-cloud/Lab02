using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab02_03
{
    public partial class Form1 : Form
    {
        // Định nghĩa giá vé cho từng dãy
        private const int PRICE_DAIY1 = 30000; // Ghế 1-5
        private const int PRICE_DAIY2 = 40000; // Ghế 6-10
        private const int PRICE_DAIY3 = 50000; // Ghế 11-15
        private const int PRICE_DAIY4 = 80000; // Ghế 16-20

        public Form1()
        {
            InitializeComponent();
            InitializeSeats();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Không cần logic đặc biệt khi load
        }

        // Tạo 20 nút ghế ngồi theo code
        private void InitializeSeats()
        {
            int seatNumber = 1;
            int buttonWidth = 60;
            int buttonHeight = 50;
            int padding = 10;
            int columns = 5;

            groupBoxSeats.Controls.Clear();
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < columns; col++)
                {
                    Button btn = new Button();
                    btn.Text = seatNumber.ToString();
                    btn.Name = "btnSeat" + seatNumber;
                    btn.Size = new Size(buttonWidth, buttonHeight);

                    // Vị trí của nút
                    btn.Location = new Point(
                        padding + col * (buttonWidth + padding * 2),
                        padding + row * (buttonHeight + padding)
                    );

                    btn.BackColor = Color.White; // Trạng thái ban đầu: Chưa bán
                    btn.Click += new EventHandler(btnSeat_Click); // Gán sự kiện chung

                    // Gán giá vé vào Tag để dễ dàng tính toán
                    btn.Tag = GetPriceForSeat(seatNumber);

                    groupBoxSeats.Controls.Add(btn);
                    seatNumber++;
                }
            }
        }

        // Xác định giá vé dựa trên số ghế
        private int GetPriceForSeat(int seatNumber)
        {
            if (seatNumber >= 1 && seatNumber <= 5) return PRICE_DAIY1;
            if (seatNumber >= 6 && seatNumber <= 10) return PRICE_DAIY2;
            if (seatNumber >= 11 && seatNumber <= 15) return PRICE_DAIY3;
            if (seatNumber >= 16 && seatNumber <= 20) return PRICE_DAIY4;
            return 0;
        }

        // Tính tổng tiền từ các ghế đang chọn (màu Blue)
        private long CalculateCurrentTotal()
        {
            long total = 0;
            foreach (Control control in groupBoxSeats.Controls)
            {
                if (control is Button btn && btn.BackColor == Color.Blue)
                {
                    total += (int)btn.Tag;
                }
            }
            return total;
        }
        // Sự kiện Click chung cho tất cả 20 nút ghế
        private void btnSeat_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.BackColor == Color.White)
            {
                // Ghế chưa bán -> đang chọn (xanh)
                btn.BackColor = Color.Blue;
            }
            else if (btn.BackColor == Color.Blue)
            {
                // Ghế đang chọn -> hủy chọn (trắng)
                btn.BackColor = Color.White;
            }
            else if (btn.BackColor == Color.Yellow)
            {
                // Ghế đã bán (vàng) -> thông báo lỗi
                MessageBox.Show("Ghế đã được bán!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Cập nhật tổng tiền ngay sau khi chọn/hủy chọn
            txtTotalPrice.Text = CalculateCurrentTotal().ToString("N0"); // Format hiển thị
        }

        // Xử lý nút CHỌN (Mua vé)
        private void btnChoose_Click(object sender, EventArgs e)
        {
            bool hasSelection = false;
            foreach (Control control in groupBoxSeats.Controls)
            {
                if (control is Button btn && btn.BackColor == Color.Blue)
                {
                    // Chuyển ghế đang chọn (xanh) thành đã bán (vàng)
                    btn.BackColor = Color.Yellow;
                    hasSelection = true;
                }
            }

            if (hasSelection)
            {
                MessageBox.Show($"Bạn đã mua vé thành công. Tổng tiền: {txtTotalPrice.Text} VNĐ", "Mua Vé Thành Công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ghế trước khi nhấn Chọn.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Cập nhật lại tổng tiền sau khi mua/không chọn
            txtTotalPrice.Text = CalculateCurrentTotal().ToString("N0");
        }

        // Xử lý nút HỦY BỎ
        private void btnCancel_Click(object sender, EventArgs e)
        {
            foreach (Control control in groupBoxSeats.Controls)
            {
                if (control is Button btn && btn.BackColor == Color.Blue)
                {
                    // Chuyển ghế đang chọn (xanh) về chưa bán (trắng)
                    btn.BackColor = Color.White;
                }
            }
            txtTotalPrice.Text = "0"; // Yêu cầu: Xuất lên Label giá trị 0
        }

        // Xử lý nút KẾT THÚC
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}