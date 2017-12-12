using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName  { get; set; }
        public string Email  { get; set; }
        public string PW  { get; set; }
        public List<RSVP> RSVPs { get; set; }
    }

    public class Wedding : BaseEntity
    {
        [Key]
        public int WeddingId { get; set; }
        public DateTime Date { get; set; }
        public string WedderOne { get; set; }
        public string WedderTwo { get; set; }
        public string Address { get; set; }
        public User Planner { get; set; }
        public int UserId { get; set; }
        public List<RSVP> Guests { get; set; }
    }

    public class RSVP
    {
        [Key]
        public int RsvpId { get; set; }
        public User Guest { get; set; }
        public int UserId { get; set; }
        public int WeddingId { get; set; }
    }
}