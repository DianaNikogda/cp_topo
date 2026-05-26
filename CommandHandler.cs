using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_TOPO
{
    class CommandHandler
    {
        private AuthService _auth = new AuthService();
        private ConnectionDB _db = new ConnectionDB();
        private CommandParser _parser = new CommandParser();
        private NoteService _notes = new NoteService();
        private LogService _log = new LogService();
        private SystemService _systemService = new SystemService();
        private UpdateService _update = new UpdateService();
        private bool isAdmin()
        {
            return Session.CurrentUser.Role == "Admin";
        }
        public void Handle(string input)
        {
            var (command, args) = _parser.Parse(input);

            switch (command)
            {
                case "login":
                    if (args.Length >= 2)
                    {
                        bool result = _auth.Login(args[0], args[1]);
                        if (result)
                            _log.AddLog(args[0], "Успешная авторизация");
                        else
                            _log.AddLog(args[0], "Ошибка авторизации");
                    }
                    else
                        Console.WriteLine("Формат команды: login <username> <password>");

                    break;

                case "logout":
                    if (Session.CurrentUser != null)
                    {
                        _log.AddLog(
                            Session.CurrentUser.Username,
                            "Выход из системы");
                    }

                    _auth.Logout();
                    break;

                case "authUser":
                    if (args.Length >= 2)
                    {
                        _auth.Register(args[0], args[1]);

                        if (Session.CurrentUser != null)
                        {
                            _log.AddLog(
                                Session.CurrentUser.Username,
                                $"Создан пользователь {args[0]}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Формат команды: authUser <username> <password>");
                    }
                    break;

                case "addNewNote":
                    if (args.Length >= 1)
                    {
                        int noteId = _notes.AddNote(args[0]);

                        if (noteId != -1)
                        {
                            Console.WriteLine($"Создана заметка, ID: {noteId}");

                            _log.AddLog(
                                Session.CurrentUser.Username,
                                $"Создана заметка ID {noteId}");
                        }
                        else
                        {
                            Console.WriteLine("Не удалось создать заметку");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Формат команды: addNewNote \"text\"");
                    }
                    break;

                case "listNotes":
                    _notes.GetAllNotes();
                    break;

                case "getNote":
                    if (args.Length >= 1 && int.TryParse(args[0], out int id))
                        _notes.GetNoteById(id);
                    else
                        Console.WriteLine("Формат команды: getNote <id>");
                    break;

                case "delNote":
                    if (args.Length >= 1 && int.TryParse(args[0], out int delId))
                    {
                        _notes.DeleteNote(delId);

                        if (Session.CurrentUser != null)
                        {
                            _log.AddLog(
                                Session.CurrentUser.Username,
                                $"Удалена заметка ID {delId}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Формат команды: delNote <id>");
                    }
                    break;

                case "updateNote":
                    if (args.Length >= 2 && int.TryParse(args[0], out int updId))
                    {
                        _notes.UpdateNote(updId, args[1]);

                        if (Session.CurrentUser != null)
                        {
                            _log.AddLog(Session.CurrentUser.Username, $"Обновлена заметка ID {updId}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Формат команды: updateNote <id> \"text\"");
                    }
                    break;

                case "help":
                    try
                    {
                        string path = "help.md";
                        string text = File.ReadAllText(path);

                        Console.WriteLine(text);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Не удалось загрузить файл инструкции");
                    }
                    break;
                case "getLogs":
                    if (isAdmin())
                        _log.GetLogs();
                    else
                        Console.WriteLine("Отказано в доступе");
                    break;
                //case "getSystemStats":
                //    if (isAdmin())
                //        _systemService.GetSystemStats();
                //    else
                //        Console.WriteLine("Отказано в доступе");
                //    break;
                case "checkForUpdates":

                    _update.CheckForUpdates();

                    break;
                case "updateAll":

                    _update.ApplyUpdate();

                    break;


                default:
                    Console.WriteLine("Введена неверная команда");
                    break;
            }
        }
    }
}