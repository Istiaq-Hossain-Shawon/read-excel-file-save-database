using System;

namespace UploadEmployee.DAL
{
    public class InventoryReportClass
    {
        public string Barcode { get; set; }
        public string SBarcode { get; set; }

        public string Description { get; set; }
        public string Brand { get; set; }
        public string Vendor { get; set; }

        public string COO { get; set; }
        public double MRP { get; set; }


        public DateTime ExpDate { get; set; }
        public string SessionId { get; set; }
        public string SessionDate { get; set; }
        public DateTime ScanStartDate { get; set; }
        public DateTime SacnEndDate { get; set; }

        public string CategoryName { get; set; }
        public string SubCategoryName { get; set; }
        public string PackSize { get; set; }


        

        public string CreatedBy { get; set; }
        public string PostedBy { get; set; }
        public double StartDate { get; set; }
        public double StartSaleDate { get; set; }
        public double StartQty { get; set; }
        public double StartSaleQty { get; set; }
        public double ScanQty { get; set; }
        public double PresentSaleQty { get; set; }
        public double AdjQty { get; set; }
        public double CPU { get; set; }
    }

}
