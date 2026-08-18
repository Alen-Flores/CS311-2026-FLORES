using CS311_CS3A_2026_Flores.Services;

public interface ILoggingService
{
  void LogAction(Log log);
}

public class LoggingService : ILoggingService
{
  public DatabaseService dbService;

  public LoggingService(DatabaseService dbService)
  {
    this.dbService = dbService;
  }

  public void LogAction(Log log)
  {
    dbService.executeSQL($"""
    INSERT INTO tbllogs (datelog, timelog, action, module, performedby, performedto)
      VALUES (
        "{log.datelog}",
        "{log.timelog}",
        "{log.action}",
        "{log.module}",
        "{log.performedby}",
        "{log.performedto}"
      )
    """);
  }
}