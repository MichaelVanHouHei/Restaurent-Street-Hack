using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using Flurl.Http;

using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace RCHACKER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {




        public int uid
        {
            get { return (int)GetValue(uidProperty); }
            set { SetValue(uidProperty, value); }
        }

        // Using a DependencyProperty as the backing store for uid.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty uidProperty =
            DependencyProperty.Register("uid", typeof(int), typeof(MainWindow), new PropertyMetadata(945779));



        public string authCode
        {
            get { return (string)GetValue(authCodeProperty); }
            set { SetValue(authCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for authCode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty authCodeProperty =
            DependencyProperty.Register("authCode", typeof(string), typeof(MainWindow),
                new PropertyMetadata("b5b0b9babf32e790b6d32faf6ad49a32"));


        public bool notLogging
        {
            get { return (bool)GetValue(notLoggingProperty); }
            set { SetValue(notLoggingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for notLogging.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty notLoggingProperty =
            DependencyProperty.Register("notLogging", typeof(bool), typeof(MainWindow), new PropertyMetadata(true));



        public int gourmentPoint
        {
            get { return (int)GetValue(gourmentPointProperty); }
            set { SetValue(gourmentPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for gourmentPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty gourmentPointProperty =
            DependencyProperty.Register("gourmentPoint", typeof(int), typeof(MainWindow), new PropertyMetadata(0));


        public int creditPoint
        {
            get { return (int)GetValue(creditPointProperty); }
            set { SetValue(creditPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for creditPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty creditPointProperty =
            DependencyProperty.Register("creditPoint", typeof(int), typeof(MainWindow), new PropertyMetadata(0));



        public int ErrorCount
        {
            get { return (int)GetValue(ErrorCountProperty); }
            set { SetValue(ErrorCountProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorCount.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorCountProperty =
            DependencyProperty.Register("ErrorCount", typeof(int), typeof(MainWindow), new PropertyMetadata(0));



        public int savedVersion
        {
            get { return (int)GetValue(savedVersionProperty); }
            set { SetValue(savedVersionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for savedVersion.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty savedVersionProperty =
            DependencyProperty.Register("savedVersion", typeof(int), typeof(MainWindow), new PropertyMetadata(1));


        public int TimeOnClient
        {
            get { return (int)GetValue(TimeOnClientProperty); }
            set { SetValue(TimeOnClientProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimeOnClient.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimeOnClientProperty =
            DependencyProperty.Register("TimeOnClient", typeof(int), typeof(MainWindow), new PropertyMetadata(0));




        public string username
        {
            get { return (string)GetValue(usernameProperty); }
            set { SetValue(usernameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for username.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty usernameProperty =
            DependencyProperty.Register("username", typeof(string), typeof(MainWindow), new PropertyMetadata(""));



        public int changedGourmentPoint
        {
            get { return (int)GetValue(changedGourmentPointProperty); }
            set { SetValue(changedGourmentPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for changedGourmentPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty changedGourmentPointProperty =
            DependencyProperty.Register("changedGourmentPoint", typeof(int), typeof(MainWindow), new PropertyMetadata(0));



        public int changedCredit
        {
            get { return (int)GetValue(changedCreditProperty); }
            set { SetValue(changedCreditProperty, value); }
        }

        // Using a DependencyProperty as the backing store for changedCredit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty changedCreditProperty =
            DependencyProperty.Register("changedCredit", typeof(int), typeof(MainWindow), new PropertyMetadata(0));




        public bool isGetProfile
        {
            get { return (bool)GetValue(isGetProfileProperty); }
            set { SetValue(isGetProfileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for isGetProfile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty isGetProfileProperty =
            DependencyProperty.Register("isGetProfile", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));

        private DateTime getProfileTime = DateTime.Now;
        private profileResponse profileStructure;
        private string sid;
        private List<int> generatedInts = new List<int>();
        private static Random rnd = new Random();
        public int getRandomID()
        {
            bool isContained = true;
            while (isContained)
            {
                var id = rnd.Next(1000, 99999);
                if (profileStructure.res.ownedItem.Any(x => x.id == id) ||
                    profileStructure.res.inventoryItem.Any(x => x.id == id) || generatedInts.Contains(id))
                {
                    isContained = true;
                }
                else
                {
                    return id;
                }
            }

            return -1;
        }
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            var timer = new DispatcherTimer();
            timer.Tick += (o, e) =>
            {
                if (getProfileTime != null)
                {
                    TimeOnClient = Convert.ToInt32((DateTime.Now - getProfileTime).TotalMilliseconds);
                }
            };
            timer.Start();


        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            notLogging = false;
            var result = await Functions.PostData(new getSidRequest() { auth_code = authCode, uid = uid });
            notLogging = true;
            var json = JObject.Parse(result);
            sid = json["res"]["sid"].ToString();
            if (string.IsNullOrEmpty(sid))
            {
                await this.ShowMessageAsync("error", "login error");
                return;
            }

            var result2 = await Functions.PostData(new postSidRequest() { uid = uid, sid = sid });
            var json2 = JObject.Parse(result2)["res"].FirstOrDefault(x => x["t"].ToString() == "3");
            if (json2 == null)
            {
                await this.ShowMessageAsync("error", "logined but get profile error");
                return;
            }
            File.WriteAllText(@Environment.CurrentDirectory + @"\profile.json", json2.ToString());
            profileStructure = JsonConvert.DeserializeObject<profileResponse>(json2.ToString());
            username = profileStructure.res.restaurantName;
            profileStructure.res.ownedItem.ForEach(x =>
            {
                var item = itemsList.FirstOrDefault(y => x.gid == y.gid);
                if (item != null)
                {
                    x.name = item.name;
                }
            });
            //    RoomItems = new ObservableCollection<OwnedItem>(profileStructure.res.ownedItem.Where(c=>c.x > 0 && c.y>0));
            RoomItems = new ObservableCollection<OwnedItem>(profileStructure.res.ownedItem);
            creditPoint = Convert.ToInt32(profileStructure.res.credits);
            gourmentPoint = Convert.ToInt32(profileStructure.res.gourmetPoint);
            isGetProfile = true;
            getProfileTime = DateTime.Now;
            savedVersion = 1;
            changedCredit = 0;
            changedGourmentPoint = 0;
            ErrorCount = 0;
            curLevel = getMyCurrentLevel(gourmentPoint);
            //    await sendSaveGame(generateBuyTodayIngradients());
        }


        public ObservableCollection<OwnedItem> RoomItems
        {
            get { return (ObservableCollection<OwnedItem>)GetValue(RoomItemsProperty); }
            set { SetValue(RoomItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RoomItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RoomItemsProperty =
            DependencyProperty.Register("RoomItems", typeof(ObservableCollection<OwnedItem>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<OwnedItem>()));



        public int Qty
        {
            get { return (int)GetValue(QtyProperty); }
            set { SetValue(QtyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Qty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QtyProperty =
            DependencyProperty.Register("Qty", typeof(int), typeof(MainWindow), new PropertyMetadata(1));

        public List<AuditChanx> generateCloneItemActions()
        {
            if (!RoomItems.Any(c => c.isSelected)) return null;
            var result = new List<AuditChanx>();
            foreach (var VARIABLE in RoomItems.Where(c => c.isSelected))
            {

                var item = this.itemsList.First(x => x.gid == Convert.ToInt32(VARIABLE.gid));
                for (int i = 0; i < Qty; i++)
                {
                    var id = getRandomID();
                    result.Add(new AuditChanx()
                    {
                        action = 22,
                        creditsDelta = 0 - item.cost,
                        //      creditsDelta=null,
                        sCreditsDelta = item.cost != 0 ? 0 : 0 - item.smcost,
                        gourmetDelta = Functions.getPurchaseItemGourmetPoint(item),
                        id = 0 - id, //buy must be random id
                        gid = Convert.ToInt32(VARIABLE.gid),
                        x = Convert.ToInt32(VARIABLE.x),
                        y = Convert.ToInt32(VARIABLE.y),
                        roomIndex = Convert.ToInt32(VARIABLE.roomIndex),
                        data = Convert.ToInt32(VARIABLE.data),
                        eid = null, //always null
                        //eid = Convert.ToInt32(VARIABLE.eid),
                    });
                }
                //result.Add(new AuditChanx()
                //{
                //    action = 5,
                //    creditsDelta = null,
                //    sCreditsDelta = null,
                //    gourmetDelta = null,
                //    id = 0 - VARIABLE.id,
                //    gid = item.gid,
                //    x = null,
                //    y = null,
                //    roomIndex = 0,
                //    data = 0,
                //    eid = null,

                //});
            }

            return result;
        }

        public List<AuditChanx> generateBuyTodayIngradients()
        {
            if (profileStructure.res.ingredientShop.Any())
            {
                //var result = new List<AuditChanx>();
                return profileStructure.res.ingredientShop.Select(ig => new AuditChanx()
                {
                    action = 34,
                    creditsDelta = Convert.ToInt32(ig.price),
                    sCreditsDelta = 0,
                    gourmetDelta = null,
                    //     qty = Convert.ToInt32( Math.Abs( 40 - ig.buyed )),
                    qty = rnd.Next(1000, 1500),
                    today = 1,
                    itemId = Convert.ToInt32(ig.id),

                }).ToList();
                //result.Add(new AuditChanx()
                //{
                //    action = 34,
                //    creditsDelta = item.cost,
                //    sCreditsDelta = item.smcost,
                //    gourmetDelta = null,
                //    qty = Qty,
                //    today = 1,
                //    itemId = item.gid,

                //});
            }

            return null;
        }
        public List<AuditChanx> generateBuyItemActions()
        {
            if (!itemsList.Any(c => c.isSelected)) return null;
            var result = new List<AuditChanx>();
            foreach (var item in this.itemsList.Where(c => c.cost != 0 && c.isSelected))
            {
                var id = getRandomID();
                if (item.groupName != "Ingredient")
                {
                    // buy action
                    for (int i = 0; i < Qty; i++)
                    {
                        result.Add(new AuditChanx()
                        {
                            action = 22,
                            creditsDelta = 0 - item.cost,
                            //      creditsDelta=null,
                            sCreditsDelta = item.cost != 0 ? 0 : 0 - item.smcost,
                            gourmetDelta = Functions.getPurchaseItemGourmetPoint(item),
                            id = 0 - id,
                            gid = item.gid,
                            x = 17,
                            y = 13,
                            roomIndex = 0,
                            data = 0,
                            eid = null,
                        });
                        // add to inventory action
                        result.Add(new AuditChanx()
                        {
                            action = 5,
                            creditsDelta = null,
                            sCreditsDelta = null,
                            gourmetDelta = null,
                            id = 0 - id,
                            gid = item.gid,
                            x = null,
                            y = null,
                            roomIndex = 0,
                            data = 0,
                            eid = null,

                        });
                    }

                }
                else if (item.groupName == "Ingredient")
                {
                    result.Add(new AuditChanx()
                    {
                        action = 34,
                        creditsDelta = item.cost,
                        sCreditsDelta = item.smcost,
                        gourmetDelta = null,
                        qty = Qty,
                        today = 1,
                        itemId = item.gid,

                    });
                }


            }

            return result;
        }

        //public async Task<bool> buyItems(bool isClone = false)
        //{
        //    if (!itemsList.Any(c => c.isSelected) && !isClone) return false;
        //    if (!RoomItems.Any(c => c.isSelected) && isClone) return false;
        //    var requestObj = new saveGameRequest()
        //    {
        //        uid = uid,
        //        sid = sid,
        //        batch = new List<_Batch>()
        //        {
        //            new _Batch()
        //            {
        //                id = uid ,
        //                restaurantName = profileStructure.res.restaurantName,
        //                gourmetPoint = gourmentPoint,
        //             //   gourmetPoint = Convert.ToInt32(profileStructure.res.gourmetPoint),
        //                trashPoint=Convert.ToInt32(profileStructure.res.trashPoint),
        //                demandPoint = 1000,
        //                credits =creditPoint,

        //               // credits =  Convert.ToInt32(profileStructure.res.credits),
        //                avatarHash = profileStructure.res.avatarHash,
        //                musicPlay =Convert.ToInt32( profileStructure.res.musicPlay),
        //                isInStreet = 1,
        //                awards = profileStructure.res.awards.Select(x=> Convert.ToInt32(x) ).ToList(),
        //                settings = profileStructure.res.settings.Select(x=> Convert.ToInt32(x) ).ToList(),
        //                saveVersion = savedVersion,
        //                timeOnClient = TimeOnClient,
        //                auditChanges = new List<AuditChanx>()
        //                {
        //                    new AuditChanx()
        //                    {
        //                        action = 35,
        //                        creditsDelta = 0,
        //                        sCreditsDelta = 0,
        //                        gourmetDelta = 0,
        //                        trash=new List<object>(),
        //                    },

        //                    new AuditChanx()
        //                    {
        //                        action = 2,
        //                        creditsDelta = 0,
        //                        sCreditsDelta = 0,
        //                        gourmetDelta = null,
        //                        qty = 0,

        //                    },
        //                    new AuditChanx()
        //                    {
        //                        action = 8,
        //                        creditsDelta = null,
        //                        sCreditsDelta = null,
        //                        gourmetDelta = null,

        //                        employees = profileStructure.res.employees.Select(y=>new _Employee()
        //                        {
        //                            notify = y.notification,
        //                            id = Convert.ToInt32( y.id),
        //                            workerHash = y.workerHash,
        //                            task= Convert.ToInt32(y.task),
        //                            happiness = 100,

        //                        }).ToList(),

        //                    },
        //                   /* new AuditChanx()
        //                    {
        //                        action =22 ,
        //                        creditsDelta = -30000,
        //                        sCreditsDelta = 0,
        //                        gourmetDelta = 50,
        //                        id = 0-itemID,
        //                        gid =3020367,
        //                        x=17,y=13,roomIndex = 0,data=0,eid=null,


        //                    },*/
        //                    //new AuditChanx()
        //                    //{
        //                    //action =5 ,
        //                    //creditsDelta = null,
        //                    //sCreditsDelta = null,
        //                    //gourmetDelta = null,
        //                    //id =  0-itemID,
        //                    //gid =3020367,
        //                    //x=null,y=null,roomIndex = 0,data=0,eid=null,

        //                    //}

        //                }
        //            }
        //        }
        //    };
        //    if (isClone)
        //    {
        //        foreach (var VARIABLE in RoomItems.Where(c=>c.isSelected))
        //        {

        //            var item = this.itemsList.First(x => x.gid == Convert.ToInt32(VARIABLE.gid));
        //            for (int i = 0; i < Qty; i++)
        //            {
        //                var id = getRandomID();
        //                requestObj.batch[0].auditChanges.Add(new AuditChanx()
        //                {
        //                    action = 22,
        //                    creditsDelta = 0 - item.cost,
        //                    //      creditsDelta=null,
        //                    sCreditsDelta = item.cost != 0 ? 0 : 0 - item.smcost,
        //                    gourmetDelta = Functions.getPurchaseItemGourmetPoint(item),
        //                    id = 0 - id, //buy must be random id
        //                    gid = Convert.ToInt32(VARIABLE.gid),
        //                    x =  Convert.ToInt32(VARIABLE.x),
        //                    y = Convert.ToInt32(VARIABLE.y),
        //                    roomIndex = Convert.ToInt32(VARIABLE.roomIndex),
        //                    data = Convert.ToInt32(VARIABLE.data),
        //                    eid = null , //always null
        //                    //eid = Convert.ToInt32(VARIABLE.eid),
        //                });
        //            }
        //        }
        //    }
        //    else
        //    {


        //        foreach (var item in this.itemsList.Where(c => c.isSelected))
        //        {
        //            var id = getRandomID();
        //            if (item.groupName != "Ingredient")
        //            {
        //                // buy action
        //                for (int i = 0; i < Qty; i++)
        //                {
        //                    requestObj.batch[0].auditChanges.Add(new AuditChanx()
        //                    {
        //                        action = 22,
        //                        creditsDelta = 0 - item.cost,
        //                        //      creditsDelta=null,
        //                        sCreditsDelta = item.cost != 0 ? 0 : 0 - item.smcost,
        //                        gourmetDelta = Functions.getPurchaseItemGourmetPoint(item),
        //                        id = 0 - id,
        //                        gid = item.gid,
        //                        x = 17,
        //                        y = 13,
        //                        roomIndex = 0,
        //                        data = 0,
        //                        eid = null,
        //                    });
        //                    // add to inventory action
        //                    requestObj.batch[0].auditChanges.Add(new AuditChanx()
        //                    {
        //                        action = 5,
        //                        creditsDelta = null,
        //                        sCreditsDelta = null,
        //                        gourmetDelta = null,
        //                        id = 0 - id,
        //                        gid = item.gid,
        //                        x = null,
        //                        y = null,
        //                        roomIndex = 0,
        //                        data = 0,
        //                        eid = null,

        //                    });
        //                }

        //            }
        //            else if (item.groupName == "Ingredient")
        //            {
        //                requestObj.batch[0].auditChanges.Add(new AuditChanx()
        //                {
        //                    action = 34,
        //                    creditsDelta = item.cost,
        //                    sCreditsDelta = item.smcost,
        //                    gourmetDelta = null,
        //                    qty = Qty,
        //                    today = 1,
        //                    itemId = item.gid,

        //                });
        //            }


        //        }
        //    }

        //    //requestObj.batch[0].auditChanges.Add();
        //    var payload = JsonConvert.SerializeObject(requestObj);

        //    var result = await Functions.PostData(payload);
        //    if (result.Contains("error"))
        //    {
        //        ErrorCount++;

        //    }
        //    else
        //    {
        //        savedVersion++;
        //        creditPoint += 100;

        //        gourmentPoint += 500;
        //        changedCredit = creditPoint - Convert.ToInt32(profileStructure.res.credits);
        //        changedGourmentPoint = gourmentPoint - Convert.ToInt32(profileStructure.res.gourmetPoint);
        //        return true;
        //    }

        //    return false;
        //}

        public saveGameRequest generateSaveGameRequest(List<AuditChanx> actions)
        {
            // gourmentPoint = 1000;
            curLevel = getMyCurrentLevel(gourmentPoint);

            var payload = new saveGameRequest()
            {
                uid = uid,
                sid = sid,
                batch = new List<object>()
                {
                    new _Batch()
                    {
                        id = uid ,
                        restaurantName = profileStructure.res.restaurantName,
                        gourmetPoint = gourmentPoint,
                     //   gourmetPoint = Convert.ToInt32(profileStructure.res.gourmetPoint),
                        trashPoint=Convert.ToInt32(profileStructure.res.trashPoint),
                        demandPoint = 1000,
                        credits =creditPoint,
          //             credits =-499499900,
                       // credits =  Convert.ToInt32(profileStructure.res.credits),
                        avatarHash = profileStructure.res.avatarHash,
                        musicPlay =Convert.ToInt32( profileStructure.res.musicPlay),
                        isInStreet = 1,
                        awards = profileStructure.res.awards.Select(x=> Convert.ToInt32(x) ).ToList(),
                        settings = profileStructure.res.settings.Select(x=> Convert.ToInt32(x) ).ToList(),
                        saveVersion = savedVersion,
                        timeOnClient = TimeOnClient,
                        auditChanges = new List<AuditChanx>()
                        {
                            new AuditChanx()
                            {
                                action = 35,
                                creditsDelta = 0,
                                sCreditsDelta = 0,
                                gourmetDelta = 0,
                                trash=new List<object>(),
                            },

                            new AuditChanx()
                            {
                                action = 2,
                                creditsDelta = 0,
                                sCreditsDelta = 0,
                                gourmetDelta = null,
                                qty = 0,

                            },
                            new AuditChanx()
                            {
                                action = 8,
                                creditsDelta = null,
                                sCreditsDelta = null,
                                gourmetDelta = null,

                                employees = profileStructure.res.employees.Select(y=>new _Employee()
                                {
                                    notify = y.notification,
                                    id = Convert.ToInt32( y.id),
                                    workerHash = y.workerHash,
                                    task= Convert.ToInt32(y.task),
                                    happiness = 100,

                                }).ToList(),

                            },
                           /* new AuditChanx()
                            {
                                action =22 ,
                                creditsDelta = -30000,
                                sCreditsDelta = 0,
                                gourmetDelta = 50,
                                id = 0-itemID,
                                gid =3020367,
                                x=17,y=13,roomIndex = 0,data=0,eid=null,


                            },*/
                            //new AuditChanx()
                            //{
                            //action =5 ,
                            //creditsDelta = null,
                            //sCreditsDelta = null,
                            //gourmetDelta = null,
                            //id =  0-itemID,
                            //gid =3020367,
                            //x=null,y=null,roomIndex = 0,data=0,eid=null,

                            //},

                        }
                    }
                }
            };
            foreach (var item in itemsList.Where(x => x.isSelected && x.cost == 0))
            {

                payload.batch.Add(new GiftAction()
                {
                    //   recipientId=uid,
                    recipientId = 945779,
                    gid = item.gid,
                    message = "hello",
                });
            }
            if (actions != null && actions.Any())
            {

                ((_Batch)payload.batch[0]).auditChanges.AddRange(actions);

            }

            if (curLevel < 75 && isBulkLevel)
            {

                var nextLevelItem = levels.First(x => x.id == curLevel + 1);
                //if(curLevel + 1 == 75)
                //{
                //    nextLevelItem.points *= 10;
                //}
                ((_Batch)payload.batch[0]).gourmetPoint = nextLevelItem.points;
                ((_Batch)payload.batch[0]).auditChanges.Add(new AuditChanx()
                {
                    action = 25,
                    creditsDelta = nextLevelItem.coinReward,
                    sCreditsDelta = 0,
                    gourmetDelta = null,
                    qty = nextLevelItem.id,
                });
                gourmentPoint = nextLevelItem.points + 1;

            }
            else if (curLevel >= 75 && isBulkLevel)
            {
                //  test bulk money and exp
                //tested its works, but  exp times 10 too over
                //any way , credit not larger than 3000
                //if (curLevel >= 75)
                //{
                //    gourmentPoint *= 10;

                //}


                //payload.batch[0].gourmetPoint = gourmentPoint;
                //payload.batch[0].auditChanges.Add(new AuditChanx()
                //{
                //    action = 25,
                //    creditsDelta = 3000,
                //    sCreditsDelta = 0,
                //    gourmetDelta = gourmentPoint,
                //    qty = 75,
                //});
            }

            return payload;
        }


        public int curLevel
        {
            get { return (int)GetValue(curLevelProperty); }
            set { SetValue(curLevelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for curLevel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty curLevelProperty =
            DependencyProperty.Register("curLevel", typeof(int), typeof(MainWindow), new PropertyMetadata(0));


        public int getMyCurrentLevel(int gp)
        {
            if (!levels.Any()) return -1;
            if (levels.Any(c => c.points == gp)) return levels.First(c => c.points == gp).id;
            if (gp >= levels.Last().points) return levels.Last().id;
            return levels.First(x => gp < x.points).id - 1;
        }

        private bool isBulkLevel = false;
        public async Task<bool> sendSaveGame(List<AuditChanx> actions)
        {
            //  var itemID = new Random().Next(1000, 4000);


            var payloadObj = generateSaveGameRequest(actions);

            var payload = JsonConvert.SerializeObject(payloadObj);
            Console.WriteLine("--------------save game begin-------------------");
            Console.WriteLine(payload);
            Console.WriteLine("--------------end save game----------------------");
            var result = await Functions.PostData(payload);
            if (result.Contains("error"))
            {
                ErrorCount++;
                return false;
            }
            else
            {
                savedVersion++;
                curLevel = getMyCurrentLevel(gourmentPoint);
                creditPoint += 100;
                gourmentPoint += 500;


                changedCredit = creditPoint - Convert.ToInt32(profileStructure.res.credits);
                changedGourmentPoint = gourmentPoint - Convert.ToInt32(profileStructure.res.gourmetPoint);
                return true;
            }


        }
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            isBulkLevel = true;
            ErrorCount = 0;
            while (true)
            {

                if (ErrorCount >= 1) //originally is 3 , now i scared of banning account
                {

                    return;
                }
                await sendSaveGame(null);
                if (curLevel >= 75) return;
                await Task.Delay(2000);
            }

        }


        //  public ObservableCollection<CustomItem> itemsList = new ObservableCollection<CustomItem>();

        public ObservableCollection<CustomItem> itemsList
        {
            get { return (ObservableCollection<CustomItem>)GetValue(itemsListProperty); }
            set { SetValue(itemsListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for itemsList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty itemsListProperty =
            DependencyProperty.Register("itemsList", typeof(ObservableCollection<CustomItem>), typeof(MainWindow), new PropertyMetadata(new ObservableCollection<CustomItem>()));


        private List<Level> levels = new List<Level>();
        private async void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // i am lazy to use code to parse these , since they wont change the name
            var result = await Functions.GetData("/res/bin/html5/restaurant.bin");
            File.WriteAllText(Environment.CurrentDirectory + @"\restaurant.json", result, Encoding.UTF8);
            var result2 = await Functions.GetData("/res/bin/html5/ingredient.bin");
            File.WriteAllText(Environment.CurrentDirectory + @"\ingredient.json", result2, Encoding.UTF8);
            var result3 = await Functions.GetData("/res/bin/html5/level_up.bin");
            File.WriteAllText(Environment.CurrentDirectory + @"\level_up.json", result3, Encoding.UTF8);
            var result4 = await Functions.GetData("/res/bin/html5/recipe.bin?v=1.14.44");
            File.WriteAllText(Environment.CurrentDirectory + @"\recipe.json", result4, Encoding.UTF8);
            var obj1 = JsonConvert.DeserializeObject<BinaryStruture>(result);
            var obj2 = JsonConvert.DeserializeObject<BinaryStruture>(result2);
            var obj3 = JsonConvert.DeserializeObject<LevelBin>(result3);
            obj3.levels.ForEach(x => x.points *= 10); //since they are coutned in double like 252.2 = 2522
            levels.AddRange(obj3.levels);
            var list = new List<CustomItem>();
            foreach (var g in obj1.groups)
            {
                list.AddRange(g.items.Select(c => new CustomItem()
                {
                    groupName = g.groupName,
                    cost = c.cost,
                    smcost = c.smcost,
                    gid = c.id,
                    level = c.level,
                    name = c.name,
                }));
            }
            foreach (var g in obj2.groups)
            {
                list.AddRange(g.items.Select(c => new CustomItem()
                {
                    groupName = g.groupName,
                    cost = c.cost,
                    //cost = 1 ,
                    //smcost = 0,
                    smcost = c.cash,
                    gid = c.id,
                    level = c.level,
                    name = c.name,
                }));
            }

            itemsList = new ObservableCollection<CustomItem>(list.Where(VARIABLE => VARIABLE.groupName != "Garden" && VARIABLE.groupName != "Music" && VARIABLE.groupName != "PondAreas" && VARIABLE.groupName != "OutsideAreaSize" && VARIABLE.groupName != "Wall" && VARIABLE.groupName != "Awards" && VARIABLE.groupName != "Trash"));
            //obj1.groups.ForEach(x=>x.items.ForEach(y=>y.cash=0));
            //var moiftied =Encoding.UTF8.GetBytes( JsonConvert.SerializeObject(obj1));
            //var moiftied_bytes = Functions.CompressZlib(moiftied);
            //File.WriteAllBytes(Environment.CurrentDirectory + @"\hack_restaurant.bin", moiftied_bytes);
            await this.ShowMessageAsync("注意事項", "1.拿好auth code,uid後請先關閉遊戲頁面,因為F5 refresh遊戲會強制save一次 , 令本外掛失效 \n 2.暫時買物件不扣錢 , 但不可以買課金和你等級不夠的物件 \n 3. 本外掛只是POC(Proof Of Concept) 本外掛作者MV對任何封號不負任可責任");
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            isBulkLevel = false;
            // bool result =await buyItems();
            bool result = await sendSaveGame(generateBuyItemActions());
            if (result)
            {
                await this.ShowMessageAsync("done", "done");
            }
            else
            {
                await this.ShowMessageAsync("error", "error");
            }
        }


        public bool isSelectAllItem
        {
            get { return (bool)GetValue(isSelectAllItemProperty); }
            set { SetValue(isSelectAllItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for isSelectAllItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty isSelectAllItemProperty =
            DependencyProperty.Register("isSelectAllItem", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var customItem in this.itemsList)
            {
                customItem.isSelected = isSelectAllItem;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            foreach (var VARIABLE in itemsList)
            {
                if (VARIABLE.groupName != "Garden" && VARIABLE.groupName != "Music" && VARIABLE.groupName != "PondArea" && VARIABLE.groupName != "OutSideAreaSize" && VARIABLE.groupName != "Wall" && VARIABLE.groupName != "Awards" && VARIABLE.groupName != "Trash")
                {
                    VARIABLE.isSelected = true;
                }
            }
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            isBulkLevel = false;
            //  bool result = await buyItems(true);
            bool result = await sendSaveGame(generateCloneItemActions());
            if (result)
            {
                await this.ShowMessageAsync("done", "done");
            }
            else
            {
                await this.ShowMessageAsync("error", "error");
            }
        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {
            isBulkLevel = false;
            bool result = await sendSaveGame(generateBuyTodayIngradients());
            if (result)
            {
                await this.ShowMessageAsync("done", "done");
            }
            else
            {
                await this.ShowMessageAsync("error", "error");
            }
        }

        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            isBulkLevel = false;
            while (true)
            {
                var b = await sendSaveGame(null);
                if (!b)
                {
                    await Task.Delay(60 * 1000);
                }
                else
                {
                    await Task.Delay(10);
                }

                // await Task.Delay(60 * 1000 * 2);
            }
        }
        static Stream stream;
        static Regex re = new Regex("user_id=([0-9]+)&auth_code=([a-z0-9]+)&", RegexOptions.Compiled);
        public async Task<(string, string)> DoReg()
        {
            var guid = Guid.NewGuid().ToString().Replace("-", "");
            FlurlClient client = new FlurlClient();
            await client.Request("https://game.streets.cafe/").GetAsync();
            var reponse = await client.WithHeaders(new
            {
                user_agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/112.0",
                accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,*/*;q=0.8",
                cookie = "PHPSESSID=27c3688adf23f98a1f2337285fdb90d9"
            }).Request("https://game.streets.cafe/").PostMultipartAsync(mp =>

                mp.AddString("do_reg", "1")
                .AddString("email", $"{guid}@gmail.com")
                .AddString("password", guid)
                .AddString("password2", guid)
                .AddString("fname", "Love")
                .AddString("lname", "Fuck")
                .AddString("sex", "2")
                //     mp.AddFile(  "photo" , stream, "girl.jpg" , "image/jpeg");
                .AddString("g_recaptcha_response", "03AL8dmw9SY8_HSsSHAGNRMWftQwMsm4GfpeFmL7CTMHQ77BaxW8h6GjbNO8tYec-Ip3FYLeulmNQyFLKgmSn3qNtRXpwOHYYkmuI2I6xsgwbRhdvjH7U1fZNghS5n7j4heceaRpOPbZHHwQ9OR--QAvwu3LN1P-r7YoPeOXULy-ZC0T3Omt_dRgVL33QWhfEf7iKgGvglW_bibkXIuBxVNTsgVV-C4y4gvzPbHdE9Jxf-8c6I3RHg-NdOcwI5xY1vVLiOB9ExX2vcRcgjIQubb6wTq2QaXEuoK4cC1CJV1nNcxHOHT98ESFhi5soxQAEdxzeWstvCXnWmrIqFa7c4BwHGPDCvhI_dDSq17dcepbMjKT04NJ-JxXnMX1Aa71_it-_dDBPkV6F2C_75_bqD7ft0cd-pA9oi1PY_QOj0OeBr5w2_Fd4rHxgp3FgOxJOcYoOlfnvVjef33jSloTz-uvXvrACeYPCQo43M20HdTNiqEef6GX3eRqV7BCPAmLtAevDAw8hDDN4XbKefTO19dtzFs5Q4H-WS9LaB0nD15zq-uZyVCNPbK_M")
            );
            var s = await reponse.GetStringAsync();
            var m = re.Matches(s).Cast<Match>().Select(c => c.Groups[1].Value).ToList();
            return (m[0], m[1]);
        }
        private async void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //byte[] buff = System.IO.File.ReadAllBytes("28246432_5c01b9e729eaa8751a6c7e3a4cc3fb67.jpg");
            //System.IO.MemoryStream ms = new System.IO.MemoryStream(buff);
            //stream = ms;
            //for (int i  = 0; i < 10;i++)
            //{
            //    //do reg
            //}
            var b = await DoReg();

        }

    }
}
