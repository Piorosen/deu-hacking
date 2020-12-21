using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace test
{
    public class CoreLecture
    {
        public List<Lecture> Week = new List<Lecture>();
        public List<Lecture> Day = new List<Lecture>();

        public void Parsing(string parsingData)
        {
            var JObj = JObject.Parse(parsingData);


            int cur_week = 0;

            foreach (var value in JObj.SelectToken("weekdownyn").Children())
            {
                cur_week = (int)value["cur_week"];
                break;
            }

            var week = JObj.SelectToken("weekdata");
            foreach (var value in week)
            {
                Lecture l = new Lecture
                {
                    ap_type = value["ap_type"].ToString(),
                    start_time = value["start_time"].ToString(),
                    end_time = value["end_time"].ToString(),
                    day_week = int.Parse(value["day_week"].ToString()),
                    curriculum_nm = value["curriculum_nm"].ToString(),
                    curriculum_cd = value["curriculum_cd"].ToString(),
                    class_no = int.Parse(value["class_no"].ToString()),
                    teacher_nm = value["teacher_nm"].ToString(),
                    room_cd = value["room_cd"].ToString(),
                    room_nm = value["room_nm"].ToString(),
                    lecture_no = int.Parse(value["lecture_no"].ToString()),
                    lecture_type = int.Parse(value["lecture_type"].ToString()),

                    
                };
                try
                {
                    l.local_name = value["local_name"].First.ToString();
                }
                catch
                {
                    l.local_name = value["local_name"].ToString();
                }
                
                Week.Add(l);
            }

            var day = JObj.SelectToken("daydata");
            foreach (var value in day)
            {
                Lecture l = new Lecture
                {
                    ap_type = value["ap_type"].ToString(),
                    start_time = value["start_time"].ToString(),
                    end_time = value["end_time"].ToString(),
                    day_week = int.Parse(value["day_week"].ToString()),
                    curriculum_nm = value["curriculum_nm"].ToString(),
                    curriculum_cd = value["curriculum_cd"].ToString(),
                    class_no = int.Parse(value["class_no"].ToString()),
                    teacher_nm = value["teacher_nm"].ToString(),
                    room_cd = value["room_cd"].ToString(),
                    room_nm = value["room_nm"].ToString(),
                    lecture_no = int.Parse(value["lecture_no"].ToString()),
                    lecture_type = int.Parse(value["lecture_type"].ToString()),
                };
                try
                {
                    l.local_name = value["local_name"].First.ToString();
                }
                catch
                {
                    l.local_name = value["local_name"].ToString();
                }
                Day.Add(l);
            }
        }
    }
}
