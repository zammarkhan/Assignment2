using Newtonsoft;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Calculation
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = true)]
        public string Calculate()
        {
            string calculator = HttpContext.Current.Request.Params["SubjectMarks"];
            List<Subjects> subjects = JsonConvert.DeserializeObject<List<Subjects>>(calculator);


            int maxmarks = subjects[0].ObtainMarks;
            int minmarks = subjects[0].ObtainMarks;
            string maxmarkstr = subjects[0].Name;
            string minmarkstr = subjects[0].Name;
            double totalmarks = 0;


            for (int i = 0; i < subjects.Count; i++)
            {
                totalmarks += subjects[i].ObtainMarks;
                if (maxmarks < subjects[i].ObtainMarks)
                {
                    maxmarks = subjects[i].ObtainMarks;
                    maxmarkstr = subjects[i].Name;
                }
                else if (minmarks > subjects[i].ObtainMarks)
                {
                    minmarks = subjects[i].ObtainMarks;
                    minmarkstr = subjects[i].Name;
                }
            }

            double percent = (totalmarks / (subjects.Count * 100)) * 100;

            Result marksheetModel = new Result
            {
                Percentage = percent,
                MinMarks = minmarks,
                MaxMarks = maxmarks,
                MinSubjectMarks = minmarkstr,
                MaxSubjectMarks = maxmarkstr
            };

            string str = JsonConvert.SerializeObject(marksheetModel);
            return str;

        }
        public class Subjects
        {
            public string Name { get; set; }
            public int ObtainMarks { get; set; }
        }

        public class Result
        {
            public int MinMarks { get; set; }
            public int MaxMarks { get; set; }
            public string MinSubjectMarks { get; set; }
            public string MaxSubjectMarks { get; set; }
            public double Percentage { get; set; }
        }
    }
}
