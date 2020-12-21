using System;
using System.Collections.Generic;
using System.Text;

namespace test
{
    public class UIManage
    {
        CoreLecture core = new CoreLecture();
        Check deu = new Check();

        int? Input()
        {
            Console.Write("> ");
            if (int.TryParse(Console.ReadLine(), out int result))
            {
                return result;
            }
            else
            {
                return null;
            }
        }
        public bool MainMenu()
        {
            Console.WriteLine("컴퓨터에서 출석체크 프로그램");
            Console.WriteLine();
            Console.WriteLine("1. 출석체크 관련");
            Console.WriteLine("2. 시간표 관련");
            Console.WriteLine("3. 종료");
            Console.WriteLine();
            
            while (true)
            {
                var result = Input();
                if (result == null)
                {
                    continue;
                }

                if (result == 1)
                {
                    Attending();
                    return false;
                }
                else if (result == 2)
                {
                    Schedule();
                    return false;
                }
                else if (result == 3)
                {
                    return true;
                }
            }
        }

        void Attending()
        {
            Console.WriteLine("출석체크 관련");
            Console.WriteLine();
            Console.WriteLine("1. 로그인");
            Console.WriteLine("2. 세션 갱신");
            Console.WriteLine("3. 강의 목록 가져오기");
            Console.WriteLine("4. 출석체크");
            Console.WriteLine("5. 뒤로가기");
            Console.WriteLine();

            while (true)
            {
                var result = Input();
                if (result == null)
                {
                    continue;
                }
                switch (result)
                {
                    case 1:
                        deu.Login();
                        break;
                    case 2:
                        deu.RefreshToken();
                        break;
                    case 3:
                        //  core.Parsing(deu.GetLecture());
                        core.Parsing(deu.GetLecture());
                        break;
                    case 4:
                        Check();
                        Console.WriteLine("출석체크 관련");
                        Console.WriteLine();
                        Console.WriteLine("1. 로그인");
                        Console.WriteLine("2. 세션 갱신");
                        Console.WriteLine("3. 강의 목록 가져오기");
                        Console.WriteLine("4. 출석체크");
                        Console.WriteLine("5. 뒤로가기");
                        Console.WriteLine();
                        break;
                    case 5:
                        return;
                }
            }
        }

        void Check()
        {
            Console.WriteLine("출석 체크");
            Console.WriteLine();
            int i = 0;
            for (; i < core.Day.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {core.Day[i].curriculum_nm} : {core.Day[i].room_nm}");
            }
            i++;
            Console.WriteLine($"{i}. 뒤로가기");
            Console.WriteLine();

            while (true)
            {
                var result = Input();
                if (result == null)
                {
                    continue;
                }
                
                if (result == i)
                {
                    return;
                }
                var lecture = core.Day[result.Value - 1];
                Console.WriteLine(deu.AttendingCheck(1
                                    , lecture.class_no
                                    , Guid.NewGuid().ToString()
                                    , lecture.lecture_type
                                    , lecture.lecture_no
                                    , lecture.local_name
                                    , lecture.room_cd));
            }
        }

        void Schedule()
        {
            Console.WriteLine("시간표 관련");
            Console.WriteLine();
            Console.WriteLine("1. 모든 수업 목록");
            Console.WriteLine("2. 오늘 수업");
            Console.WriteLine("3. 뒤로가기");
            Console.WriteLine();


            while (true)
            {
                var result = Input();
                if (result == null)
                {
                    continue;
                }

                switch (result)
                {
                    case 1:
                        for (int i = 0; i < core.Week.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}번째 강좌 : ");
                            Console.WriteLine($"강의 이름 : {core.Week[i].curriculum_nm}");
                            Console.WriteLine($"강의 장소 : {core.Week[i].room_nm}");
                            Console.WriteLine($"수업 시간 : {core.Week[i].start_time} ~ {core.Week[i].end_time}");
                            Console.WriteLine();
                        }
                        break;
                    case 2:
                        for (int i = 0; i < core.Day.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}번째 강좌 : ");
                            Console.WriteLine($"강의 이름 : {core.Day[i].curriculum_nm}");
                            Console.WriteLine($"강의 장소 : {core.Day[i].room_nm}");
                            Console.WriteLine($"수업 시간 : {core.Day[i].start_time} ~ {core.Day[i].end_time}");
                            Console.WriteLine();
                        }
                        break;
                    case 3:
                        return;
                }
            }
        }

    }
}
