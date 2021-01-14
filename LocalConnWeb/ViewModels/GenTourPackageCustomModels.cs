using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.CustomModels;
using LocalConnWeb.Models;

namespace LocalConnWeb.ViewModels
{
   public class TourPackageBookingManageModel
   {
       public utblClientEnquirie ClientEnquirie { get; set; }
       public utblClientEnquiryItinerarie ClientEQIrinerary { get; set; }
       public IEnumerable<utblMstHotelType> HotelTypes { get; set; }
       public IEnumerable<utblMstCabType> CabTypes { get; set; }
       public ClientEnquiryView ClientEnqInfoView { get; set; }
       public IEnumerable<ClientEnquiryItineraryView> ClientEnqItineraryView { get; set; }
       public IEnumerable<ClientEnquiryActivityView> ClientEnqActivityView { get; set; }
       public List<ClientEnqActivities> ClientEnqActivityList { get; set; }
  
   }
   public class ClientEnquiryView
   {
       public string EnquiryCode { get; set; }
       public string ClientName { get; set; }
       public string ClientEmail { get; set; }
       public string ClientPhoneNo { get; set; }
       public long RefPackageID { get; set; }
       public int NoOfDays { get; set; }
       public DateTime DateOfArrival { get; set; }
       public string NoOfAdult { get; set; }
       public string NoOfChildren { get; set; }
       public string HotelTypeName { get; set; }
       public string CabTypeName { get; set; }
   }
   public class ClientEnquiryItineraryView
   {
       public long ClientItineraryID { get; set; }
       public string EnquiryCode { get; set; }
       public int DayNo { get; set; }
       public long ItineraryID { get; set; }
       public string ItineraryName { get; set; }
       public string ItineraryRemarks { get; set; }
       public long OvernightDestinationID { get; set; }
       public string DestinationName { get; set; }
       public string OvernightDestination { get; set; }
       public bool SightSeeing { get; set; }
       public bool Breakfast { get; set; }
       public bool lunch { get; set; }
       public bool Dinner { get; set; }
       public bool Stay { get; set; }
   }
   public class ClientEnquiryActivityView
   {
       public long ClientItineraryID { get; set; }
       public string EnquiryCode { get; set; }
       public long ActivityID { get; set; }
       public string ActivityName { get; set; }
   }
   public class SearchModel
   {
       public string Where { get; set; }
       public List<long> TourType { get; set; }
       public int PageNo { get; set; }
       public int PageSize { get; set; }
   }
}