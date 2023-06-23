using System.Collections.Generic;
using System.Linq;
using TraceabilityHandler;
using Logger = AlberEOL.CustomClasses.Logger;

namespace AlberEOL.Base
{
    public interface IProduct
    {
        TraceabilityHandler.Product TraceProduct { get; }
        ProductType ProductType { get; set; }
        OperationDetails OperationDetails { get; set; }
        ProductState ProductState { get; set; }
        GeneralMessage Message { get; set; }
        int ErrorCodeItemIndex { get; set; }
        ErrorCode ErrorCode { get; set; }
        Parameter GetParameter(string parametername);
        Property GetProperty(string propertyname);
        int SetOperationDetails(string userid);
        void RefreshProductState(string operationTypeName, int productTypeId);
        void AddOperationResult();
        bool AddOperationDetail(OperationDetail detail);
    }
    internal class Product : IProduct
    {
        public TraceabilityHandler.Product TraceProduct { get; set; }
        public ProductType ProductType { get; set; }
        public OperationDetails OperationDetails { get; set; }
        public ProductState ProductState { get; set; }
        public GeneralMessage Message { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public int ErrorCodeItemIndex { get; set; }

        public Product(string serial)
        {
            try
            {
                TraceProduct = API.GetProductBySerialID(serial);
            }
            catch (TraceabilityException ex)
            {
                this.TraceProduct = null;
                this.Message = new WarningMessage($"TRACE EXCEPTION -API.GetProductBySerialID: {ex.Message}");
                this.ErrorCode = new ErrorCode("TRACE1", this.Message);
                Logger.WriteExceptionLog(this.Message.Text, this.ErrorCode.Code);
                throw new TraceabilityException($"{this.ErrorCode.Code} - {this.ErrorCode.Message}");
            }
            if (string.IsNullOrEmpty(TraceProduct.ProductID))
            {
                this.TraceProduct = null;
                this.Message = new WarningMessage($"Nem található termék ezzel a szériaszámmal a trace-ben: {serial}");
                this.ErrorCode = new ErrorCode("TRACE2", this.Message);
                Logger.WriteExceptionLog(this.Message.Text, this.ErrorCode.Code);
            }
 
            this.OperationDetails = new OperationDetails
            {
                Details = new List<OperationDetail>()
            };
            this.ErrorCodeItemIndex = 0;
        }

        public void RefreshProductState(string operationTypeName, int productTypeId)
        {
            try
            {
                this.ProductState = API.GetProductState(this.TraceProduct.ProductID, operationTypeName, productTypeId);
            }
            catch (TraceabilityException ex)
            {
                this.Message = new WarningMessage($"TRACE EXCEPTION -API.GetProductState: {ex.Message}");
                this.ErrorCode = new ErrorCode("TRACE1", this.Message);
                Logger.WriteExceptionLog(this.Message.Text, this.ErrorCode.Code);
                throw new TraceabilityException($"{this.ErrorCode.Code} - {this.ErrorCode.Message}");
            }
            if (this.ProductState.OK)
            {
                this.ProductType = ProductState.ProductType;
            }
            else
            {
                this.Message = new WarningMessage($"Nem megfelelő termékállapot: {ProductState.Message} {operationTypeName}");
                this.ErrorCode = new ErrorCode("TRACE4", this.Message);
                Logger.WriteGeneralLog(this.Message.Text, this.ErrorCode.Code);
                return;
            }
        }

        public bool AddOperationDetail(OperationDetail operationDetail)
        {
            if (operationDetail == null)
            {
                this.Message = new WarningMessage("OperationDetail is null!");

                this.Message = new WarningMessage($"Hiányzó OperationDetail");
                this.ErrorCode = new ErrorCode("TRACE5", this.Message);
                Logger.WriteGeneralLog(this.Message.Text, this.ErrorCode.Code);
                return false;
            }
            try
            {
                OperationDetails.Details.Add(operationDetail);
            }
            catch (TraceabilityException ex)
            {
                this.Message = new WarningMessage($"TRACE EXCEPTION -OperationDetails.Details:Add: {ex.Message}");
                this.ErrorCode = new ErrorCode("TRACE1", this.Message);
                Logger.WriteExceptionLog(this.Message.Text, this.ErrorCode.Code);
                throw new TraceabilityException($"{this.ErrorCode.Code} - {this.ErrorCode.Message}");
            }
            return true;
        }

        /// <summary>
        /// Paraméterek kinyerése a productstateresult-ból
        /// </summary>
        /// <param name="parametername"></param>
        /// <returns></returns>
        public Parameter GetParameter(string parametername)
        {
            return ProductState.Parameters.CurrentParameters.Where(p => p.Name.ToLower() == parametername.ToLower()).FirstOrDefault();
        }

        public Property GetProperty(string propertyname)
        {
            return ProductState.ProductType.Properties.Where(p => p.PropertyName.ToLower() == propertyname.ToLower()).FirstOrDefault();
        }

        public int SetOperationDetails(string userid)
        {
            OperationDetails.Judgement = (OperationDetails.Details.FirstOrDefault(i => i.Passed == 0) == null) ? 0 : 1;
            OperationDetails.ParamVersion = ProductState.Parameters.CurrentParamVersion.VersionID.ToString();
            OperationDetails.PropertyVersionID = ProductState.ProductType.Properties[0].VersionID;
            OperationDetails.UserID = userid;
            OperationDetails.ProductID = ProductState.ProductID;
            OperationDetails.OperationID = ProductState.Parameters.CurrentParameters[0].OperationTypeID;
            return OperationDetails.Judgement;
        }

        public void AddOperationResult()
        {
            this.ProductState = API.AddOperationResult(OperationDetails);
        }
    }
}
