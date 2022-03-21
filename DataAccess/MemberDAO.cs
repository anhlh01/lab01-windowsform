using System;
using System.Collections.Generic;
using BusinessObject;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.IO;

namespace DataAccess
{
    public class MemberDAO
    {
        //Read file json to get admin
        public MemberObject LoadJson()
        {
            MemberObject member;
            using (StreamReader r = new StreamReader(@"appsettings.json"))
            {
                string json = r.ReadToEnd();
                member = JsonConvert.DeserializeObject<MemberObject>(json);
            }
            return member;
        }
        //Initialize member list
        private static List<MemberObject> MemberList = new List<MemberObject>()
        {
            new MemberObject
            {
                MemberID = 1,
                MemberName = "anh",
                City = "Ha Noi",
                Email = "anh1@gmail.com",
                Country = "Vietnam",
                Password = "123456"
            },
            new MemberObject
            {
                MemberID = 2,
                MemberName = "quang",
                City = "Ha Noi",
                Email = "anh2@gmail.com",
                Country = "My",
                Password = "123456"
            },
            new MemberObject
            {
                MemberID = 3,
                MemberName = "long",
                City = "Ha Noi",
                Email = "anh3@gmail.com",
                Country = "Phap",
                Password = "123456"
            },
            new MemberObject
            {
                MemberID = 4,
                MemberName = "Nguyen Van d",
                City = "Ha Noi",
                Email = "anh4@gmail.com",
                Country = "Vietnam",
                Password = "123456"
            },
        };
        
        //Using Singleton Pattern
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }
        //Get list member
        public List<MemberObject> GetMemberList => MemberList;
        //Login a member 
        public MemberObject LoginMember (string email, string password)
        {
            MemberObject member = MemberList.Where(m => m.Email.Equals(email.ToLower()) && m.Password.Equals(password)).FirstOrDefault();
            return member;
        }
        //Get list city
        public List<string> GetListCity()
        {
            return MemberList.Select(m => m.City).Distinct().ToList();
        }
        //Get list country
        public List<string> GetListCountry()
        {
            return MemberList.Select(m => m.Country).Distinct().ToList();
        }
        //Get a member by id
        public MemberObject GetMemberByID (int memberID)
        {
            MemberObject member = MemberList.SingleOrDefault(pro => pro.MemberID == memberID);
            return member;
        }
        //Get list member by name
        public List<MemberObject> GetMemberByName (string memberName)
        {
            string name = memberName.Trim().ToLower();
            List<MemberObject> list = MemberList.Where(m => m.MemberName.ToLower().Contains(memberName)).ToList();
            return list;
        }
        //Get list member by city
        public List<MemberObject> GetMemberByCity(string city)
        {
            List<MemberObject> list = MemberList.Where(m => m.City.Equals(city)).ToList();
            return list;
        }
        //Get list member by country
        public List<MemberObject> GetMemberByCountry(string country)
        {
            List<MemberObject> list = MemberList.Where(m => m.Country.Equals(country)).ToList();
            return list;
        }
        //Sort list member descending order by name
        public List<MemberObject> GetListMemberDescendingByName()
        {
            List<MemberObject> list = MemberList.OrderByDescending(m => m.MemberName).ToList();
            return list;
        }
        //Add a new member
        public void AddMember(MemberObject member)
        {

            MemberObject m = GetMemberByID(member.MemberID);
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(member.Email);
            if (m != null)
            {
                throw new Exception("Member is already exists.");                
            }
            else if (member.MemberID <= 0)
            {
                throw new Exception("Member id must large than 0.");
            }
            else if (member.MemberName.Length < 2 || member.MemberName.Length > 50)
            {

                throw new Exception("Member name must be between 2 and 50 character.");
            }

            else if (!match.Success)
            {
                
                throw new Exception("Invalid email format.");
            }
            
            
            
            else if (member.Password.Length < 2 || member.Password.Length > 10)
            {

                throw new Exception("Password must be between 2 and 10 character.");
            }
            
            else if (member.City.Length == 0)
            {

                throw new Exception("City can not be empty!");
            }
            
            else if (member.Country.Length == 0)
            {

                throw new Exception("Country can not be empty!");
            }
            
            else 
                MemberList.Add(member);
        }
        //Update a member
        public int UpdateMember (MemberObject member)
        {
            MemberObject m = GetMemberByID(member.MemberID);
            if (m != null)
            {
                var index = MemberList.IndexOf(m);
                MemberList[index] = member;

                return index;
            }
            else
            {
                throw new Exception("Member does not already exists.");
            }
        }
        //Remove a member
        public void RemoveMember(int memberID)
        {
            MemberObject member = GetMemberByID(memberID);
            if (member != null)
            {
                MemberList.Remove(member);
            }
            else
            {
                throw new Exception("Member does not already exists.");
            }
        }
    }
}
