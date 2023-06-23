using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RCHACKER
{
    /*action list
     com_shketlabs_rpc_streetscafe_AuditChangeAction.creditShakeTree = 1;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.creditChangeBuyMeal = 2;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.purchaseInventoryItem = 3;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.sellOwnedItem = 4;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.fromGameToInventory = 5;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.fromInventoryToGame = 6;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.saveFloor = 7;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.updateEmployee = 8;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.lockIngredient = 9;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.deleteCollaborateItem = 10;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.activatePack = 11;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.manageMails = 16;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.hireEmployee = 17;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.fireEmployee = 18;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.sellInventoryItem = 19;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.openMail = 20;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.deleteMail = 21;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.purchaseOwnedItem = 22;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.saveOwnedItem = 23;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.creditChangeOffLine = 24;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.lvlUpdate = 25;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.purchasePerks = 32;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.addRecipe = 33;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.purchaseIngredient = 34;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.pickUpTrash = 35;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.creditOutRestaurant = 36;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.selectRecipe = 37;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.creditFunctionalItem = 48;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.filterPerkItem = 103;
    com_shketlabs_rpc_streetscafe_AuditChangeAction.purchaseOudoorExtension = 104;
    com_shketlabs_rpc_streetscafe_PurchasableItem.ITEM_TYPE_INVENTORY = 1;
    com_shketlabs_rpc_streetscafe_PurchasableItem.ITEM_TYPE_INGREDIENT = 2;
    com_shketlabs_rpc_streetscafe_PurchasableItem.ITEM_TYPE_CASH = 3;
     */
    /* t action
     *  com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_getAllFriends = 2;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_getUserProfile = 3;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_getUsers = 4;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_saveProfile = 5;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_wateringPlant = 6;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_saveHarvest = 7;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_feedingFish = 8;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_catchingFish = 9;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_buyingPondArea = 10;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_swapIngredient = 17;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_sendMail = 19;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_getMails = 20;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_quizzReply = 25;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_buyMystryBox = 32;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_storeImage = 34;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_rankRestaurant = 35;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_firstTimeVisitFriend = 36;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_getRandomStreetUsers = 37;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_getGourmetStreetUsers = 38;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_buyStarMoney = 111;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_shoutIngredient = 112;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_postGiftOnWall = 133;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_postShoutOnWall = 134;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_wallPostSuccess = 135;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_purchaseCashItem = 247;
    com_shketlabs_rpc_streetscafe_RpcClient.CALL_TYPE_getPurchasableItems = 250;
     */
    public class Batch
    {
        public int t { get; set; }
    }

    public class postSidRequest
    {
        public int t = 255;
        public int uid { get; set; }
        public string sid { get; set; }

        public List<Batch> batch = new List<Batch>()
        {
            new Batch() {t = 249}, //serverTime
            new Batch() {t = 3}, //mydata
         //   new Batch() {t = 2},//fucking friends
        //    new Batch() {t = 20},//unknown
         //   new Batch() {t = 250}, //adware
        };
    }
    public class OutdoorAreaSize
    {
        public double sizeX { get; set; }
        public double sizeY { get; set; }
        public double id { get; set; }
    }

    public class Employee
    {
        public double task { get; set; }
        public double happiness { get; set; }
        public bool notification { get; set; }
        public double id { get; set; }
        public string workerHash { get; set; }
        public double lastSave { get; set; }
        public string avatarHash { get; set; }
    }

    public class Ingredient
    {
        public double id { get; set; }
        public bool @lock { get; set; }
        public double n { get; set; }
    }

    public class OwnedItem
    {
        public int id { get; set; }
        public double gid { get; set; }
        public double? eid { get; set; }
        public double? data { get; set; }
        public double? x { get; set; }
        public double? y { get; set; }
        public double? roomIndex { get; set; }
        [JsonIgnore]
        public bool isSelected { get; set; }
        [JsonIgnore]
        public string name { get; set; }
    }

    public class InventoryItem
    {
        public int id { get; set; }
        public double n { get; set; }
        public double isSelected { get; set; }
    }

    public class PondArea
    {
        public double gid { get; set; }
        public double areaNum { get; set; }
    }

    public class IngredientShop
    {
        public double id { get; set; }
        public double price { get; set; }
        public double starPrice { get; set; }
        public double expired { get; set; }
        public double buyed { get; set; }
    }

    public class Res
    {
        public double id { get; set; }
        public string firstName { get; set; }
        public string fullName { get; set; }
        public double gender { get; set; }
        public string restaurantName { get; set; }
        public double demandPoint { get; set; }
        public double gourmetPoint { get; set; }
        public string avatarHash { get; set; }
        public double outdoorLevel { get; set; }
       // public List<OutdoorAreaSize> outdoorAreaSize { get; set; }
        public double rank { get; set; }
        public string imageUrl { get; set; }
        public double trashPoint { get; set; }
        public List<double> awards { get; set; }
        public List<Employee> employees { get; set; }
        public List<Ingredient> ingredients { get; set; }
        public List<OwnedItem> ownedItem { get; set; }
        public List<InventoryItem> inventoryItem { get; set; }
        public List<object> floor { get; set; }
        public List<object> outdoorFloor { get; set; }
        public double isInStreet { get; set; }
        public double playCount { get; set; }
        public double musicPlay { get; set; }
        public double nbVote { get; set; }
        public double totalMark { get; set; }
        public object perks { get; set; }
        public List<object> gardenPlots { get; set; }
        public List<PondArea> pondAreas { get; set; }
        public object shout { get; set; }
        public object build { get; set; }
        public double paid { get; set; }
        public double level { get; set; }
        public double credits { get; set; }
        public double starMoney { get; set; }
        public List<double> settings { get; set; }
        public double lastSave { get; set; }
        public double invites { get; set; }
        public double lastSurveyTime { get; set; }
        public List<double> visitedFriend { get; set; }
        public List<double> selectedRecipes { get; set; }
        public double albumId { get; set; }
        public double lastFilterCharge { get; set; }
        public List<object> postedFriends { get; set; }
        public List<IngredientShop> ingredientShop { get; set; }
        public double totalBuilds { get; set; }
        public object collaborativeItems { get; set; }
        public double offlineEarning { get; set; }
    }
     
    public class GiftAction   
    {
        public int recipientId { get; set; }
        public int gid { get; set; }
        public object secondGlobalItemId = null;
        public int itemId = 0;
        public string message { get; set; }
        public int type  = 4;
        public int t = 19;
    }
    //public class collaborativeItems
    //{
    //    public int? gid { get; set; }
    //    public int? helps { get; set; }
    //    public string hash { get; set; }
    //}
    public class profileResponse
    {
        //should be t = 3 
        public double t { get; set; }
        public Res res { get; set; }
    }
    public class _Employee
    {
        public int id { get; set; }
        public double happiness { get; set; }
        public int task { get; set; }
        public bool notify { get; set; }
        public string workerHash { get; set; }
    }

    public class AuditChanx
    {
        public int action { get; set; }
        public int? creditsDelta { get; set; }
        public int? sCreditsDelta { get; set; }
        public int? gourmetDelta { get; set; }
        public List<object> trash { get; set; }
        public int? id { get; set; }
        // action 22 = buy item, 23 = move item , 5 put to inventory
        public bool ShouldSerializeid()
        {
            return action == 22 || action == 23 || action == 5; 
        }
        public int? gid { get; set; }
        public bool ShouldSerializegid()
        {
            return action == 22 || action == 23 || action == 5;
        }
        public int? x { get; set; }
        public bool ShouldSerializex()
        {
            return action == 22 || action == 23 || action == 5;
        }
        public int? y { get; set; }
        public bool ShouldSerializey()
        {
            return action == 22 || action == 23 || action == 5;
        }
        public int? roomIndex { get; set; }
        public bool ShouldSerializeroomIndex()
        {
            return action == 22 || action == 23 || action == 5;
        }
        public int? data { get; set; }
        public bool ShouldSerializeroomdata()
        {
            return action == 22 || action == 23 || action == 5;
        }
        public int?  eid { get; set; }
        public bool ShouldSerializeroomeid()
        {
            return action == 22 || action == 23 || action == 5;
        }

        public bool ShouldSerializetrash()
        {
            return action == 35;
        }
        //note that itemID is for buying igradien only
        public int itemId { get; set; }
        public bool ShouldSerializeitemId()
        {
            return action ==34;
        }
        public int? qty { get; set; }
        public bool ShouldSerializeqty()
        {
            //denote that action 25 is levelup  and your level = qty 
            return action == 2 || action ==34 || action == 25;
        }
        public int? today { get; set; } = 0;
        //action 34 is buy ingradient
        public bool ShouldSerializetoday()
        {
            return action == 34;
        }
        public List<_Employee> employees { get; set; }
        public bool ShouldSerializeemployees()
        {
            return action == 8;
        }
    }

    public class _Batch
    {
        public int id { get; set; }
     //   public int scredits = 100;
        public string restaurantName { get; set; }
        public int gourmetPoint { get; set; }
        public int trashPoint { get; set; }
        public int demandPoint { get; set; }
        public string avatarHash { get; set; }
        public int musicPlay { get; set; }
        public int isInStreet { get; set; }
        public int credits { get; set; }
        public List<int> awards { get; set; }
        public List<int> settings { get; set; }
        public int saveVersion { get; set; }
        public int timeOnClient { get; set; }
        [JsonProperty(PropertyName = "auditChanges")]
        public List<AuditChanx> auditChanges { get; set; }
         

        public int t = 5;//save fucntion 
    }

    public class saveGameRequest
    {
        public int t = 255;
        public int uid { get; set; }
        public string sid { get; set; }
        [JsonProperty(PropertyName = "batch")]
        public List<object> batch { get; set; }
    }
    public class getSidRequest
    {
      //  [JsonProperty(Order = 1)]
        public int t = 1;
      //  [JsonProperty(Order = 2)]
        public int uid { get; set; }
     //   [JsonProperty(Order = 3)]
        public object sid = null;
      //  [JsonProperty(Order = 4)]
        public string auth_code { get; set; }
     //   [JsonProperty(Order = 5)]
        public string platform = "html5canvas";
     //   [JsonProperty(Order = 6)]
        public string version = "HTML5 2.0.1";
    }

}
