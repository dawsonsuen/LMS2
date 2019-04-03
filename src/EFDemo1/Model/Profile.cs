using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace EFDemo1.Model
{
    public class Profile
    {
        public static Profile CreateProfileFromBody(Profile profile)
        {
            Profile newProfile = new Profile();
            newProfile.Name = profile.Name;
            newProfile.UserId = profile.UserId;
            newProfile.PhoneNumber = profile.PhoneNumber;
            newProfile.EmailAddress = profile.EmailAddress;
            return newProfile;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public Profile()
        {
        }
    }
}
