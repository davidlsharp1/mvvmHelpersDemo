using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmHelpers;
using mvvmHelpersDemo.Model;
using Newtonsoft.Json;
using BarrelSQL = MonkeyCache.SQLite.Barrel;


namespace mvvmHelpersDemo.ViewModel
{
    public class MonkeyViewModel : BaseViewModel
    {

        public ObservableRangeCollection<Monkey> Monkeys { get; set; } = new ObservableRangeCollection<Monkey>();

        public MonkeyViewModel()
        {
            BarrelSQL.ApplicationId = "davesharp";
        }


        public void ClearMonkeys()
        {
            Monkeys.Clear();
        }

        public async Task GetMonkeys()
        {
            try
            {
                if (BarrelSQL.Current.IsExpired("monkeysjson"))
                {
                    Monkeys.Clear();
                    var client = new HttpClient();
                    var json = await client.GetStringAsync("https://raw.githubusercontent.com/jamesmontemagno/app-monkeys/master/MonkeysApp/monkeydata.json");
                    var items = JsonConvert.DeserializeObject<List<Monkey>>(json);
                    BarrelSQL.Current.Add("monkeysjson", json, TimeSpan.FromMinutes(5));
                    Monkeys.ReplaceRange(items);
                }
                else
                {
                    // retrieve data from cache
                    Monkeys.Clear();
                    var items = BarrelSQL.Current.Get<List<Monkey>>("monkeysjson");
//                    var items = JsonConvert.DeserializeObject<List<Monkey>>(json);
                    Monkeys.ReplaceRange(items);
                }
            }

            catch (Exception ex)
            {
                Debug.WriteLine($"ERROR: {ex.Message}");
            }

        }




    }
}
