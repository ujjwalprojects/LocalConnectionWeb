using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.CustomModels
{

    public class TourPackageManageModel1
    {
        public utblTourPackage Package { get; set; }
        public IEnumerable<utblMstPackageType> PackageTypes { get; set; }
    }
    public class TourPackageManageModel2
    {
        public PackageBriefInfo Package { get; set; }
        public List<utblTourPackageItinerary> Itineraries { get; set; }
        public IEnumerable<utblMstitinerarie> ItineraryList { get; set; }
        public IEnumerable<utblMstDestination> DestinationList { get; set; }
    }
    public class TourPackageImageAPIVM
    {
        public IEnumerable<utblTourPackageImage> PackageImages { get; set; }
        public int TotalRecords { get; set; }
    }
    public class TourPackageImageVM
    {
        public PackageBriefInfo Package { get; set; }
        public IEnumerable<utblTourPackageImage> PackageImages { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class PackageBriefInfo
    {
        public long PackageID { get; set; }
        public string PackageName { get; set; }
        public int TotalDays { get; set; }
    }
    public class ImageStrs
    {
        public string PhotoNormal { get; set; }
        public string PhotoThumb { get; set; }
    }
    public class PackageImageSaveModel
    {
        public PackageBriefInfo Package { get; set; }
        public utblTourPackageImage Image { get; set; }
        public ImageStrs ImageStrs { get; set; }
    }
    public class PackageActivities
    {
        public long PackageActivityID { get; set; }
        public long PackageID { get; set; }
        public long ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDesc { get; set; }
        public decimal ActivityFare { get; set; }
        public bool IsSelected { get; set; }
    }
    public class PackageInclusions
    {
        public long PackageInclusionID { get; set; }
        public long PackageID { get; set; }
        public long InclusionID { get; set; }
        public string InclusionName { get; set; }
        public string InclusionType { get; set; }
        public decimal InclusionFare { get; set; }
        public bool IsSelected { get; set; }
    }
    public class TourPackageManageModel3
    {
        public PackageBriefInfo Package { get; set; }
        public List<PackageActivities> Activities { get; set; }
        public List<PackageInclusions> Inclusions { get; set; }
        public long PackageID { get; set; }
    }
    public class PackageExclusions
    {
        public long PackageExclusionID { get; set; }
        public long PackageID { get; set; }
        public long ExclusionID { get; set; }
        public string ExclusionName { get; set; }
        public bool IsSelected { get; set; }
    }
    public class PackageTerms
    {
        public long PackageTermsID { get; set; }
        public long PackageID { get; set; }
        public long TermID { get; set; }
        public string TermName { get; set; }
        public bool IsSelected { get; set; }
    }
    public class PackageCancellations
    {
        public long PackageCancID { get; set; }
        public long PackageID { get; set; }
        public long CancellationID { get; set; }
        public string CancellationDesc { get; set; }
        public bool IsSelected { get; set; }
    }
    public class TourPackageManageModel4
    {
        public PackageBriefInfo Package { get; set; }
        public List<PackageExclusions> Exclusions { get; set; }
        public List<PackageTerms> Terms { get; set; }
        public List<PackageCancellations> Cancellations { get; set; }
        public long PackageID { get; set; }
    }

    public class TourPackageView
    {
        public long PackageID { get; set; }
        public string PackageName { get; set; }
        public long PackageTypeID { get; set; }
        public string PackageTypeName { get; set; }
        public string PackageRouting { get; set; }
        public string PickupPoint { get; set; }
        public string DropPoint { get; set; }
        public int TotalDays { get; set; }
        public decimal BaseFare { get; set; }
        public string PackageDesc { get; set; }
        public decimal FinalFare { get; set; }
        public long PackageHitCount { get; set; }
        public bool IsActive { get; set; }
        public string LinkText { get; set; }
        public string MetaText { get; set; }
        public string MetaDesc { get; set; }
        public string FarePer { get; set; }
    }
    public class TourPackageAPIVM
    {
        public IEnumerable<TourPackageView> Packages { get; set; }
        public int TotalRecords { get; set; }
    }
    public class TourPackageVM
    {
        public IEnumerable<TourPackageView> Packages { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class PackageItineraryView
    {
        public long PackageItineraryID { get; set; }
        public long PackageID { get; set; }
        public long ItineraryID { get; set; }
        public string ItineraryName { get; set; }
        public int DayNo { get; set; }
        public string ItineraryRemarks { get; set; }
        public long OvernightDestinationID { get; set; }
        public string DestinationName { get; set; }
        public string OvernightDestination { get; set; }
        public bool SightSeeing { get; set; }
        public bool Breakfast { get; set; }
        public bool Lunch { get; set; }
        public bool Dinner { get; set; }
        public bool Stay { get; set; }
    }
   
    public class PackageItinerarypck
    {
      
        public long PackageItineraryID { get; set; }
        public long PackageID { get; set; }
        public long ItineraryID { get; set; }
        public string ItineraryName { get; set; }
        public int DayNo { get; set; }
        public string ItineraryRemarks { get; set; }
        public long? OvernightDestinationID { get; set; }
        public string DestinationName { get; set; }
        public string OvernightDestination { get; set; }
        public bool SightSeeing { get; set; }
        public bool Breakfast { get; set; }
        public bool Lunch { get; set; }
        public bool Dinner { get; set; }
        public bool Stay { get; set; }
        public bool IncludeDay { get; set; }
    }
    public class TourPackageFullVM
    {
        public TourPackageView Package { get; set; }
        public IEnumerable<PackageItineraryView> Itineraries { get; set; }
        public IEnumerable<utblTourPackageImage> Images { get; set; }
        public IEnumerable<PackageActivities> Activities { get; set; }
        public IEnumerable<PackageInclusions> Inclusions { get; set; }
        public IEnumerable<PackageExclusions> Exclusions { get; set; }
        public IEnumerable<PackageTerms> Terms { get; set; }
        public IEnumerable<PackageCancellations> Cancellations { get; set; }

    }


    /////////Package Enquires & Bookings /////////////
    public class TourPackageBookEnquiry
    {
        public string Enquirycode { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
        public string ClientPhoneNo { get; set; }
        public long RefPackageID { get; set; }
        public int NoOfDays { get; set; }
        public string Remarks { get; set; }
        public DateTime DateOfArrival { get; set; }
        public string NoOfAdult { get; set; }
        public string NoOfChildren { get; set; }
        public string HotelTypeName { get; set; }
        public string CabTypeName { get; set; }
        public bool IsDirectBooking { get; set; }
        public string Status { get; set; }
        public string PackageName { get; set; }
        public decimal BaseFare { get; set; }
        public string PackageDesc { get; set; }
        public decimal FinalFare { get; set; }
        public string FarePer { get; set; }
    }
    public class TourPackageBookEnquiryAPIVM
    {
        public IEnumerable<TourPackageBookEnquiry> Enquiries { get; set; }
        public int TotalRecords { get; set; }
    }
    public class TourPackageBookEnquiryVM
    {
        public IEnumerable<TourPackageBookEnquiry> Enquiries { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class EnquiryItineraryView
    {
        public long ClientItineraryID { get; set; }
        public string EnquiryCode { get; set; }
        public long RefPackageID { get; set; }
        public long ItineraryID { get; set; }
        public string ItineraryName { get; set; }
        public int DayNo { get; set; }
        public string ItineraryRemarks { get; set; }
        public long? OvernightDestinationID { get; set; }
        public string DestinationName { get; set; }
        public string OvernightDestination { get; set; }
        public bool SightSeeing { get; set; }
        public bool Breakfast { get; set; }
        public bool Lunch { get; set; }
        public bool Dinner { get; set; }
        public bool Stay { get; set; }
    }
    public class EnquiryActivitiesView
    {
        public long ClientActivityID { get; set; }
        public long RefPackageID { get; set; }
        public string EnquiryCode { get; set; }
        public long ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDesc { get; set; }
    }
    public class EnquiryVM
    {
        public TourPackageBookEnquiry Enquiry { get; set; }
        public IEnumerable<EnquiryItineraryView> Itineraries { get; set; }
        public IEnumerable<EnquiryActivitiesView> Activities { get; set; }
    }
}