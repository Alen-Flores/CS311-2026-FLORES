using System;

public record class Log(
  string datelog,
  string timelog,
  string action,
  string module,
  string performedby,
  string performedto
)
{
  public static Log WithCurrentTimeStamp(string action, string module, string performedby, string performedto)
  {
    return new Log(
        DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("h:mm tt") // ToShortTimeString causes an error
              , action, module, performedby, performedto
    );
  }
};