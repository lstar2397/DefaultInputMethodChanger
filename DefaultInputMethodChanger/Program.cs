using DefaultInputMethodChanger;

var delay = 100;
var rules = new HashSet<IntPtr>();

while (true)
{
    var handle = WindowsAPI.GetForegroundWindow();
    var className = WindowsAPIUtility.GetClassName(handle);
    var windowText = WindowsAPIUtility.GetWindowText(handle);

    if (className == "#32770" &&
        windowText == "다른 이름으로 저장")
    {
        if (!rules.Contains(handle))
        {
            Console.WriteLine($"{DateTime.Now} \"{windowText}\" 대화창을 발견하였습니다.");

            var imeConversionMode = WindowsAPIUtility.GetIMEConversionMode(handle);
            if (imeConversionMode == IMEConversionMode.ALPHANUMERIC)
            {
                WindowsAPIUtility.ToggleInputMethod(handle);
                Console.WriteLine($" -> 입력 모드를 한글로 전환하였습니다!");
            }

            rules.Add(handle);
        }
    }

    if (delay > 0)
        Thread.Sleep(delay);
}
