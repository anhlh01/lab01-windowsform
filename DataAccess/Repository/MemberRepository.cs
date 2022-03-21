using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public MemberObject LoadJson() => MemberDAO.Instance.LoadJson();
        public MemberObject LoginMember(string email, string password)
            => MemberDAO.Instance.LoginMember(email, password);

        void IMemberRepository.AddMember(MemberObject member)
            => MemberDAO.Instance.AddMember(member);

        IEnumerable<string> IMemberRepository.GetListCity()
            => MemberDAO.Instance.GetListCity();

        IEnumerable<string> IMemberRepository.GetListCountry()
            => MemberDAO.Instance.GetListCountry();

        IEnumerable<MemberObject> IMemberRepository.GetListMemberDescendingByName()
            => MemberDAO.Instance.GetListMemberDescendingByName();

        IEnumerable<MemberObject> IMemberRepository.GetMemberByCity(string city)
            => MemberDAO.Instance.GetMemberByCity(city);

        IEnumerable<MemberObject> IMemberRepository.GetMemberByCountry(string country)
            => MemberDAO.Instance.GetMemberByCountry(country);

        MemberObject IMemberRepository.GetMemberByID(int memberID)
            => MemberDAO.Instance.GetMemberByID(memberID);

        IEnumerable<MemberObject> IMemberRepository.GetMemberByName(string name)
            => MemberDAO.Instance.GetMemberByName(name);

        IEnumerable<MemberObject> IMemberRepository.GetMembers()
            => MemberDAO.Instance.GetMemberList;
        MemberObject IMemberRepository.LoadJson() => MemberDAO.Instance.LoadJson();
        MemberObject IMemberRepository.LoginMember(string email, string password)
            => MemberDAO.Instance.LoginMember(email, password);

        void IMemberRepository.RemoveMember(int memberID)
            => MemberDAO.Instance.RemoveMember(memberID);

        int IMemberRepository.UpdateMember(MemberObject member) => MemberDAO.Instance.UpdateMember(member);
    }
}
