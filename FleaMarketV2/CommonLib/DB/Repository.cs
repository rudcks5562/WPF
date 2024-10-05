using CommonLib.Service;
using CommonLib.DB;
using FleaMarketV2Client.Model;

using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CommonLib.Model;

namespace CommonLib.DB
{
    public class Repository // CRUD 관련 함수 모음 data access class
    {
        public string connectionString = "";
        public Repository()
        {
            DBConnectionManager DBM = new DBConnectionManager();
            this.connectionString = DBM.connectionString;
        }
        public ObservableCollection<UserInfo> GetAllUser()
        {
            
            ObservableCollection<UserInfo> tmpCollection = new ObservableCollection<UserInfo>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM dbo.UserTable";// 유저테이블의 ID와 이름값을 바탕으로 list별 아이템 정보를 재정의한다. 
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserInfo tmpUser = new UserInfo();
                       
                        tmpUser.UserId = reader["UserId"].ToString();
                        tmpUser.UserPassword = reader["Password"].ToString();
                        tmpCollection.Add(tmpUser);
                    }
                }
                connection.Close();
                return tmpCollection;
            }
        }
        public ObservableCollection<SUserModel> GetAllData()
        {
            ObservableCollection<SUserModel> tmp_collection = new ObservableCollection<SUserModel>();//캐시 
            
            ObservableCollection<SItemModel> tmp_ItemList = new ObservableCollection<SItemModel>();// 유저가 들고있는 아이템목록
            // 일종의 유저 집합
            // 각 유저별 이름과 아이템리스트 그리고 그 아이템리스트에 대한 정보를 받아와야함. -> 캐시 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM dbo.UserTable";// 유저테이블의 ID와 이름값을 바탕으로 list별 아이템 정보를 재정의한다. 
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                   while (reader.Read())
                    {
                        SUserModel tmp_SUserModel = new SUserModel();// 한 유저별 조사 (임시 유저 정의)-> 아이템리스트 여기다 넣으면될듯?
                        tmp_SUserModel.Cmd = "Refresh";// getall의 목적은 최신데이터 갱신 하나이다.
                        tmp_SUserModel.ItemModels = new ObservableCollection<SItemModel>();
                        string User_ID = reader.GetString(0);
                        //tmp_SUserModel.UserName = reader.GetString(1);
                       // tmp_ItemList = new ObservableCollection<SItemModel>();
                        string secQuery = "SELECT UserId,ItemId From dbo.UserItemRelationTable WHERE UserId ='"+ User_ID+"'";
                        // 한 유저별로 그 유저와 연관된 아이템의 리스트를 위해 정보를 빼온다.                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
                        SqlConnection SecConnection = new SqlConnection(connectionString);
                        SecConnection.Open();
                        SqlCommand SecCommand = new SqlCommand(secQuery, SecConnection);
                        tmp_ItemList = new ObservableCollection<SItemModel>();
                        using (SqlDataReader secReader = SecCommand.ExecuteReader())
                        {
                            while (secReader.Read())
                            {
                                string InUid = secReader.GetString(0);
                                // 연관된 아이템 리스트를 정리하여 세번째 쿼리로 아이템정보 일일히 뽑아와서 리스트에 넣기
                                int inIid = secReader.GetInt32(1);
                                if (InUid == User_ID)
                                {
                                    SItemModel tmp_Item = new SItemModel();
                                    tmp_Item.ItemCode = inIid.ToString();
                                    SqlConnection ThirdConnection = new SqlConnection(connectionString);
                                    ThirdConnection.Open();
                                    string thirdQuery = "SELECT * FROM dbo.ItemTable WHERE ItemCode = '" + tmp_Item.ItemCode+"'";
                                    SqlCommand thirdCommand = new SqlCommand(thirdQuery, ThirdConnection);

                                    using (SqlDataReader thirdReader = thirdCommand.ExecuteReader())
                                    {
                                        while (thirdReader.Read())
                                        {
                                            tmp_Item.ItemCode = thirdReader["ItemCode"].ToString();
                                            tmp_Item.ItemName = thirdReader["ItemName"].ToString();
                                            tmp_Item.Cost = (int)thirdReader["ItemCost"];
                                            tmp_Item.Description = thirdReader["ItemDescription"].ToString();
                                            tmp_Item.IsOnMarket = (int)thirdReader["IsOnMarket"];
                                           // tmp_Item.ImageData = (byte[])thirdReader["ItemImage"];
                                            object itemImageObj = thirdReader["ItemImage"];
                                            if (itemImageObj != DBNull.Value)
                                            {
                                                tmp_Item.ImageData = (byte[])itemImageObj;
                                            }
                                            else
                                            {
                                                tmp_Item.ImageData = null; // 또는 기본값 설정 등을 수행할 수 있습니다.
                                            }
                                            tmp_Item.RegLog = thirdReader["ItemRegisterDate"].ToString();
                                            tmp_Item.Seller = thirdReader["ItemSeller"].ToString();
                                        }// 아이템코드는 고유기 때문에 while은 한 번 돌음.
                                    }
                                    tmp_ItemList.Add(tmp_Item);// 아이템코드에 해당되는(연관된) 아이템을 유저가 들 아이템리스트에 저장.
                                }
                            }
                        }
                        tmp_SUserModel.ItemModels=(tmp_ItemList);
                        tmp_collection.Add(tmp_SUserModel);
                    }
                }
               
            }
            return tmp_collection;
        }
        public bool CheckUserAccount(string inputId,string inputPw)
        {
            bool res = false;
            string outputId = "";
            string outputPw = "";
            string query = "SELECT UserId,Password FROM UserTable WHERE UserId=@id and Password=@pw";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", inputId);
                    cmd.Parameters.AddWithValue("@pw", inputPw);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            outputId = reader["UserId"].ToString();
                            outputPw = reader["Password"].ToString();
                        }
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    connection.Close();
                }
            }

            if(inputId.Equals(outputId)&& inputPw.Equals(outputPw))
            {
                return true;
            }
            else if(inputPw.Equals("") && outputId is not null)// 중복체크 true시 같은 계정 존재
            {
                if (outputId.Equals(inputId))
                {
                    return true;
                }

            }
            return res;
        }
        public bool CheckUserAccountDuplicate(string inputId)
        {
            bool res = false;
            string outputId = "";
            string query = "SELECT UserId FROM UserTable WHERE UserId=@id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", inputId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            outputId = reader["UserId"].ToString();
                        }
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    connection.Close();
                }
            }

            if (inputId.Equals(outputId))
            {
                return true;
            }
            return res;
        }
        public void InsertUser(string userId, string password)// user테이블에 유저정보 추가
        {
            string query = "INSERT INTO dbo.UserTable" + "(UserId,Password)" + "VALUES (@userid,@userpw)";
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@userpw", password);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch(Exception e)
                {
                    connection.Close();
                }
            }
        }
        public void UpdateUser(string userId, string password)// 유저 업데이트 
        {
            string query = "UPDATE dbo.UserTable SET " + "Password=@userpw WHERE UserId=@userid";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@userpw", password);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    connection.Close();
                }
            }
        }
        public void DeleteUser(string userId)
        {
            string query = "DELETE FROM dbo.UserTable Where UserId = @userid ";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    connection.Close();
                }
            }
        }
        /*
        public void InsertItem(SUserModel InputModel)// 아이템 등록
        {
            //user는 로그인을 한 상태이므로 유저테이블에 반드시 있는 가정하에 아이템을 먼저 아이템테이블에 삽입해주고 이후 관계테이블에 관계를 입력한다.
            if (InputModel.UserName == "ADMIN")
            {
                Action adminAct = () =>
                {
                    //item만 삽입
                    InsertItemPhaseOne(InputModel);
                };
                adminAct();

            }
            else
            {
                Action userAct = () =>
                {
                    //관계성과 아이템 삽입
                    InsertItemPhaseOne(InputModel);
                };
                userAct();
            }
        }*/
        public void InsertItemPhaseOne(SUserModel InputModel)// 아이템 테이블에 아이템 넣는 작업1-> 아이템의 등록은 1개씩 이뤄짐.
        {
            SItemModel Item = InputModel.ItemModels.ElementAt(0);
            string query = "INSERT INTO dbo.ItemTable" + "(ItemName,ItemCost,ItemDescription,ItemImage,ItemRegisterDate,ItemSeller,IsOnMarket)" + "VALUES (@itemname, @itemcost, @itemdesc,@itemimage,@itemregdate,@itemseller,@isonmarket);SELECT SCOPE_IDENTITY();";
            int newItemID=0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@itemname", Item.ItemName);
                    cmd.Parameters.AddWithValue("@itemcost", Item.Cost);
                    cmd.Parameters.AddWithValue("@itemdesc", Item.Description);
                    cmd.Parameters.AddWithValue("@itemimage", Item.ImageData);
                    cmd.Parameters.AddWithValue("@itemregdate", DateTime.Parse(Item.RegLog));
                    cmd.Parameters.AddWithValue("@itemseller", Item.Seller);
                    cmd.Parameters.AddWithValue("@isonmarket", Item.IsOnMarket);
                    //cmd.ExecuteNonQuery();
                    newItemID = System.Convert.ToInt32(cmd.ExecuteScalar());
                    connection.Close();
                }
                catch (Exception e)
                {
                    Trace.WriteLine($"{e}");
                    connection.Close();
                }
            }
            query = "INSERT INTO dbo.UserItemRelationTable" + "(UserId,ItemId)" + "VALUES (@userid, @itemid)";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@userid", InputModel.UserID);
                    cmd.Parameters.AddWithValue("@itemid", newItemID);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                    connection.Close();
                }
            }
        }
        public void DeleteItem(SUserModel InputModel)// 삭제시 
        {
            // 역순으로 삭제해야 하기 때문에 관계테이블의 외래키 부터 모두 삭제한 후 아이템을 테이블에서 제거한다.
            DeleteItemPhaseTwo(InputModel);
            DeleteItemPhaseOne(InputModel);
        }
        public void DeleteItemPhaseOne(SUserModel InputModel)// 아이템 지우기
        {
            SItemModel Item = InputModel.ItemModels.ElementAt(0);// 다수의 아이템이 들어온다는 예외를 찾으면 foreach문 적용할 것.
            string query = "DELETE FROM dbo.ItemTable WHERE ItemCode = @itemcode";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@itemcode", int.Parse(Item.ItemCode));
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    connection.Close();
                }
            }
        }
        public void DeleteItemPhaseTwo(SUserModel InputModel)// 관계 테이블 지우기
        {
            SItemModel Item = InputModel.ItemModels.ElementAt(0);
            string query = "DELETE FROM dbo.UserItemRelationTable WHERE ItemId = @itemid and UserId = @userid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try    // 
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@itemid", int.Parse(Item.ItemCode));
                    cmd.Parameters.AddWithValue("@userid", InputModel.UserID);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {
                    connection.Close();
                }
            }
        }

        public void UpdateItem(SUserModel changedModel,bool SoldFlag)// 판매된 경우와 아이템 등록 모두 사용가능한.
        {
            // 아이템 내부 정보의 변경인지 아이템 소유자의 변경인지 판별해야한다. 
            // 내부만 변경(해당사항 x??)이면 update문을 내부에서만 적용해준다. 소유주의 변경인 경우(구매) 관계테이블의 관계를 기존 것을 지우고 새로운 관계를 입력한다. , 등록이 되는 경우
            // 기존의 아이템 정보 변경으로 보면 될 것이고, 판매가 된 경우에는 소유주의 변경을 보면 될 것이다.

            SItemModel tmpItem = changedModel.ItemModels.ElementAt(0);

            if (SoldFlag)// 판매의 경우 소유주 변경되므로(반드시 아이템테이블에는 존재하는 전제) 관계테이블 변화
            {
                string query = "DELETE FROM dbo.UserItemRelationTable WHERE ItemId= @itemcode";// 판매자의 릴레이션은 제거, 구매자의 릴레이션은 추가. 아래로부터 수정하기0725
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@itemcode", int.Parse(tmpItem.ItemCode));
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                    }
                }// 귀속된 아이템 정보를 지운 후

                query = "Insert INTO dbo.UserItemRelationTable (UserId,ItemId) VALUES (@userid,@itemid)";// 구매자의 릴레이션을 다시 insert한다.
                    
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@itemid", int.Parse(tmpItem.ItemCode));
                        cmd.Parameters.AddWithValue("@userid", changedModel.UserID);
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                    }
                }
                query = "UPDATE dbo.ItemTable SET IsOnMarket=0 WHERE ItemCode=@itemcode";// 마켓에서 판매된 직후이므로 마켓대상 제외

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@itemcode", int.Parse(tmpItem.ItemCode));
                        cmd.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                    }
                }
                
            }
            else// 등록의 경우-> 기존 아이템 테이블에 존재하는가? 존재하면 seller만 바꿔서 , 존재하지 않는다면 insert해주고.
            {
                string query = "SELECT * FROM ItemTable WHERE ItemCode = @itemcode";
                string result = null;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        SqlCommand cmd = new SqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@itemcode", int.Parse(tmpItem.ItemCode));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result = reader["ItemCode"].ToString();
                            }
                        }
                        connection.Close();
                    }
                    catch (Exception e)
                    {
                        connection.Close();
                    }
                }

                if (result==null)// 아이템 없는경우 인서트
                {
                    InsertItemPhaseOne(changedModel);

                }
                else// 기존 아이템 있는 경우 변경
                {
                    query = "UPDATE dbo.ItemTable SET ItemName = @itemname , ItemCost=@itemcost, ItemDescription=@itemdesc ,ItemImage=@itemimg, ItemRegisterDate=@itemvaliddate ,ItemSeller=@itemseller,IsOnMarket=@isonmarket WHERE ItemCode = @itemcode";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {

                            connection.Open();
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.Parameters.AddWithValue("@itemname", tmpItem.ItemName);
                            cmd.Parameters.AddWithValue("@itemcost", tmpItem.Cost);
                            cmd.Parameters.AddWithValue("@itemdesc", tmpItem.Description);
                            cmd.Parameters.AddWithValue("@itemimg", 0);
                            cmd.Parameters.AddWithValue("@itemvaliddate", tmpItem.RegLog);
                            cmd.Parameters.AddWithValue("@itemseller", tmpItem.Seller);
                            cmd.Parameters.AddWithValue("@itemcode", result);
                            cmd.Parameters.AddWithValue("@isonmarket", 1);

                            cmd.ExecuteNonQuery();
                            connection.Close();
                        }
                        catch (Exception e)
                        {
                            Trace.WriteLine($"{e.Message}");
                            connection.Close();
                        }
                    }

                }


            }
            

        }
        public ObservableCollection<SItemModel> SearchMyItem(string sellerName)//ObservableCollection<ItemModel> 꼴의 객체 리턴할 예정
        {
            string query = "SELECT * FROM dbo.ItemTable WHERE ItemSeller = @myname and IsOnMarket=1";
            ObservableCollection<SItemModel> result = new ObservableCollection<SItemModel>();
            SItemModel tmpItem = new SItemModel();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@myname", sellerName);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tmpItem = new SItemModel();
                            tmpItem.ItemCode = reader["ItemCode"].ToString();
                            tmpItem.ItemName = reader["ItemName"].ToString();
                            tmpItem.Cost = (int)reader["ItemCost"];
                            tmpItem.Description = reader["ItemDescription"].ToString();
                            tmpItem.Seller = reader["ItemSeller"].ToString();
                            tmpItem.IsOnMarket = (int)reader["IsOnMarket"];
                            if (reader["ItemImage"] != DBNull.Value)
                            {
                                tmpItem.ImageData = (byte[])reader["ItemImage"];
                            }
                            else
                            {
                                tmpItem.ImageData = null; // 또는 기본값 설정 등을 수행할 수 있습니다.
                            }
                            tmpItem.RegLog = reader["ItemRegisterDate"].ToString();
                            result.Add(tmpItem);
                        }
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    connection.Close();
                }
            }//??????????????????
            return result;
        }
        public ObservableCollection<SItemModel> SearchItemByName(string searchWord)//ObservableCollection<ItemModel> 꼴의 객체 리턴할 예정
        {
            ObservableCollection<SItemModel> result = new ObservableCollection<SItemModel>();
            string query = "SELECT * FROM dbo.ItemTable WHERE ItemName LIKE '%" + searchWord + "%' and IsOnMarket = 1";
            SItemModel tmpItem;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query, connection);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tmpItem = new SItemModel();
                            tmpItem.ItemCode = reader["ItemCode"].ToString();
                            tmpItem.ItemName = reader["ItemName"].ToString();
                            tmpItem.Cost = (int)reader["ItemCost"];
                            tmpItem.Description = reader["ItemDescription"].ToString();
                            tmpItem.Seller = reader["ItemSeller"].ToString();
                            tmpItem.IsOnMarket = (int)reader["IsOnMarket"];
                            if (reader["ItemImage"] != DBNull.Value)
                            {
                                tmpItem.ImageData = (byte[])reader["ItemImage"];
                            }
                            else
                            {
                                tmpItem.ImageData = null; // 또는 기본값 설정 등을 수행할 수 있습니다.
                            }
                            tmpItem.RegLog = reader["ItemRegisterDate"].ToString();
                            result.Add(tmpItem);
                        }
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    connection.Close();
                    Trace.WriteLine(e);
                }
            }
            return result;
        }
        public void RecentDBToCache()// dll 모듈화 할 때 매개변수로 받는 기법 쓰면 분리 가능 obj X
        {
            CacheManager.GetInstance().SendCache.SCacheData= GetAllData();// DB에서 추출한 기록데이터 
            // 서버의 최신 데이터를 DB의 과거 데이터와 대조하여 변경점 식별시 쿼리 적용하여 업데이트 할 것.
        }

        public void Identify(SUserModel InputModel)// user1명에 대한 변화 분석 -> 클라이언트가 서버로 자신의 변화를 옮길 때?-> DB부터 고치고 캐시반영.
        {
            string who = (InputModel.UserID);// 유저 누구가 
            string cmd = InputModel.Cmd;// 

            if (cmd == "Delete")// 삭제시 -> 모델 내부 아이템 원소가 삭제됨을 암시         
            {
                DeleteItem(InputModel);
            }
            else if (cmd == "Purchase")// 구매시 -> 모델 내 아이템 원소를 모델에 명시된 사용자가 구매함을 암시 <구매 판매 작용 잘 할것.>
            {
                // 모델 내부 아이템 코드를 추출
                // 기존 관계테이블의 아이템관계를 제거, 현 모델에 명시된 유저와의 관계를 삽입.
                UpdateItem(InputModel,true);

            }
            else if (cmd == "Regist")// 등록시 -> 모델 내 아이템을 
            {
                // 기존 아이템테이블에 존재하는지 판별 ? (있으면: 아이템 내부 속성 seller만 변경해주기) : (없으면 itemtable과 relationtable 에 insert해주기 )
                UpdateItem(InputModel, false);
            }

        }
    }
}
