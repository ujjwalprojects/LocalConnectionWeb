using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using LocalConnWeb.Models;

namespace LocalConnWeb.CustomModels
{
    public class GenTourPackageView
    {
        public long PackageID { get; set; }
        public string PackageName { get; set; }
        public string PackageTypeName { get; set; }
        public int Days { get; set; }
        public int Nights { get; set; }
        public decimal FinalFare { get; set; }
        public string LinkText { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoNormalPath { get; set; }
        public string FarePer { get; set; }
    }
    public class GenOfferTourPackageView
    {
        public long PackageID { get; set; }
        public string PackageName { get; set; }
        public string PackageTypeName { get; set; }
        public int Days { get; set; }
        public int Nights { get; set; }
        public decimal FinalFare { get; set; }
        public decimal DiscountFare { get; set; }
        public Int16 OfferDiscount { get; set; }
        public string LinkText { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoNormalPath { get; set; }
        public string FarePer { get; set; }
    }
    public class GenTourPackageImage
    {
        public long PackageImageID { get; set; }
        public long PackageID { get; set; }
        public bool IsPackageCover { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoNormalPath { get; set; }
        public string PhotoCaption { get; set; }
    }
    public class GenTourPackageDtlView
    {
        public long PackageID { get; set; }
        public string PackageName { get; set; }
        public string PackageTypeName { get; set; }
        public string PackageRouting { get; set; }
        public string PickupPoint { get; set; }
        public string DropPoint { get; set; }
        public int Days { get; set; }
        public int Nights { get; set; }
        public string PackageDesc { get; set; }
        public decimal FinalFare { get; set; }
        public string FarePer { get; set; }
    }
    public class Itinerariesstatus
    {
        public bool Nochange { get; set; }
        public bool Notrequired { get; set; }

    }
    public class ClientEnqActivities
    {
        public long ClientActivityID { get; set; }
        public string EnquiryCode { get; set; }
        public long RefPackageID { get; set; }
        public long ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDesc { get; set; }
        public decimal ActivityFare { get; set; }
        public bool IsSelected { get; set; }
    }
    public class GenTourPackageAPIVM
    {

        public GenTourPackageView GenTourPackageView { get; set; }
        public GenTourPackageImage GenTourPackageCoverImgView { get; set; }
        public GenTourPackageDtlView GenTourPackageDtlView { get; set; }
        public IEnumerable<PackageItineraryView> Itineraries { get; set; }
        public IEnumerable<PackageInclusions> Inclusions { get; set; }
        public IEnumerable<PackageExclusions> Exclusions { get; set; }
        public IEnumerable<PackageActivities> AdvActivity { get; set; }
        public IEnumerable<GenTourPackageImage> GalleryImage { get; set; }
        public IEnumerable<PackageTerms> Terms { get; set; }
        public IEnumerable<PackageCancellations> Cancel { get; set; }
        public IEnumerable<GenTourPackageView> TourPackageList { get; set; }
        public IEnumerable<GenOfferTourPackageView> OfferPackageList { get; set; }
        public utblClientEnquirie ClientEnquiry { get; set; }
        public List<utblClientEnquiryItinerarie> ClientEnqItinerary { get; set; }
        public List<utblClientEnquiryActivitie> ClientEnqActivity { get; set; }
        public IEnumerable<utblMstHotelType> HotelTypes { get; set; }
        public IEnumerable<utblMstCabType> CabTypes { get; set; }
        public IEnumerable<utblMstitinerarie> ItineraryList { get; set; }
        public IEnumerable<utblMstDestination> DestinationList { get; set; }
        public List<PackageActivities> ActivityList { get; set; }
        public List<ClientEnqActivities> ClientEnqActivityList { get; set; }
        public List<PackageItineraryView> Itinerariespck { get; set; }
        public Itinerariesstatus Itinerariesstatus { get; set; }
        public int TotalRecords { get; set; }
        public string linkname { get; set; }
    }

    public class GenTourPackageVM
    {
        public IEnumerable<GenTourPackageView> PackageList { get; set; }
        public IEnumerable<GenOfferTourPackageView> OfferPackageList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class GenSearchModel
    {
        public string Where { get; set; }
        public List<long> TourType { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
    public class GenTourPackageSearchModel
    {
        public IEnumerable<GenTourPackageView> PackageList { get; set; }
        public int TotalRecords { get; set; }
        public GenSearchModel Search { get; set; }
        public IEnumerable<utblMstPackageType> TourTypes { get; set; }
    }
}