using System;

namespace UploadEmployee.DAL
{
    public class EntityClass
    {
    }

    public class InventoryTrackingReportItem
    {
        public string Barcode { get; set; }
        public string SBarcode { get; set; }

        public string Description { get; set; }
        public string COO { get; set; }
        public double MRP { get; set; }


        public DateTime ExpDate { get; set; }
        public string SessionId { get; set; }
        public string SessionDate { get; set; }
        public DateTime ScanStartDate { get; set; }
        public DateTime SacnEndDate { get; set; }

        public string Department { get; set; }
        public string SubDepartment { get; set; }
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

    public class DepartmentListItem
    {
        public string DepartmentName { get; set; }

    }
    public class ProductListItem
    {
        public string ProductName { get; set; }

    }
}
