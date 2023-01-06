public class Product_Option
{
    public int Id;
    public string Name;
    public int Option_Id;
    public string Option_Name;
    public int OneBox_Total_Count; //한박스에 몇개 들어있는지
    public int Person_Per_Count; //한명당 몇개 가는지
    public int Total_Box; //총 박스 수량
    public int Sell_Count; //몇번 지급되었는지
    public int Error_Count; //불량 갯수
    public int Person_Per_Price; //한번 지급할때마다 얼마인지
}
