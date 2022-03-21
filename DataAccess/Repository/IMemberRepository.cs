using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess.Repository
{
    public interface IMemberRepository
    {
        IEnumerable<MemberObject> GetMembers();
        MemberObject GetMemberByID(int memberID);
        IEnumerable<MemberObject> GetMemberByName(string name);
        IEnumerable<MemberObject> GetMemberByCity(string city);
        IEnumerable<MemberObject> GetMemberByCountry(string country);
        IEnumerable<MemberObject> GetListMemberDescendingByName();
        void AddMember(MemberObject member);
        int UpdateMember(MemberObject member);
        void RemoveMember(int memberID);
        MemberObject LoginMember(string email, string password);
        IEnumerable<string> GetListCountry();
        IEnumerable<string> GetListCity();
        MemberObject LoadJson();
    }
}
