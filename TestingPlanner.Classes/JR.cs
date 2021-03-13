﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TestingPlanner
{
    public class JR
    {
        private int idRequest;
        private string jrNumber;
        private string jrStatus;
        private string requester;
        private string eutProjectname;
        private string eutPartnr;
        private string hydraProjectnumber;
        private bool internRequest;
        private string grossWeight;
        private string netWeight;
        private bool battery;
        private string link;
        private string remarks;
        private string barcoDivision;
        private string jobNature;
        private DateTime expEnddate;
        private string pvgResp;
        private DateTime availabilityDate;

        public JR()
        {
          
        }
        public int IdRequest { get => idRequest; set => idRequest = value; }
        public string JrNumber { get => jrNumber; set => jrNumber = value; }
        public string JrStatus { get => jrStatus; set => jrStatus = value; }
        public string Requester { get => requester; set => requester = value; }
        public string EutProjectname { get => eutProjectname; set => eutProjectname = value; }
        public string EutPartnr { get => eutPartnr; set => eutPartnr = value; }
        public string HydraProjectnumber { get => hydraProjectnumber; set => hydraProjectnumber = value; }
        public bool InternRequest { get => internRequest; set => internRequest = value; }
        public string GrossWeight { get => grossWeight; set => grossWeight = value; }
        public string NetWeight { get => netWeight; set => netWeight = value; }
        public bool Battery { get => battery; set => battery = value; }
        public string Link { get => link; set => link = value; }
        public string Remarks { get => remarks; set => remarks = value; }
        public string BarcoDivision { get => barcoDivision; set => barcoDivision = value; }
        public string JobNature { get => jobNature; set => jobNature = value; }
        public DateTime ExpEnddate { get => expEnddate; set => expEnddate = value; }
        public string PvgResp { get => pvgResp; set => pvgResp = value; }
        public DateTime AvaibilityDate { get => availabilityDate; set => availabilityDate = value; }
    }
}
