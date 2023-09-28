using MauiPetsApp.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiPets.Mvvm.Models
{
    public class ContactGroup : List<ContactoVM>
    {
        public string Name { get; private set; }

        public ContactGroup(string name, List<ContactoVM> contacts) : base(contacts)
        {
            Name = name;
        }
    }
}
