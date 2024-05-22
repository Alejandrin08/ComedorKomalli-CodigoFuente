using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Views.Pages;
using KomalliServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using System.Xml.Linq;

namespace KomalliEmployee.Controller {
    public class LogbookController : ILogbook {
        /**
         * <summary>
         * Este método añade los comentarios dentro de la bitácora.
         * </summary>
         * <param name="logbookModel">se pasa el objeto como referencia para un registro dentro de la base</param>
         * <returns>Si la operación se realiza correctamente, retorna 0. Si occurre algún error, retorna -1.</returns>
         */
        public int AddCommentary(LogbookModel logbookModel) {
            int result = -1;
            try {
                using (var context = new KomalliEntities())
                using (var transaction = context.Database.BeginTransaction()) {
                    var comment = new KomalliServer.Logbook() {
                        Date = logbookModel.Date,
                        Section = logbookModel.Section,
                        Commentary = logbookModel.Commentary,
                        NoPersonalEmployee = logbookModel.NoPersonalEmployee,
                    };

                    context.Logbook.Add(comment);
                    try {
                        result = context.SaveChanges();
                    } catch (DbUpdateException ex) {
                        transaction.Rollback();
                        LoggerManager.Instance.LogFatal("Error al actualizar la base de datos. Error añadiendo el comentario", ex);
                    }
                    transaction.Commit();
                }
            } catch (DbUpdateException ex) {
                LoggerManager.Instance.LogFatal("Error al añadir el comentario a la bitacora", ex);
            }
            return result;
        }

        /**
        * <summary>
        * Este método obtiene los comentarios añadidos por un personal.
        * </summary>
        * <param name="noPersonal">número del personal de quién lo realizó</param>
        * <returns>Retorna una lista de comentarios.</returns>
        */
        public List<LogbookModel> GetEmployeeComments(string noPersonal) {
            List<LogbookModel> comments = new List<LogbookModel>();
            try {
                using (var context = new KomalliEntities()) {
                    var query = context.Logbook
                        .Where(comment => comment.NoPersonalEmployee == noPersonal).ToList();
                    
                    foreach (var comment in query) {
                        comments.Add(new LogbookModel {  
                            Date = comment.Date,
                            Commentary = comment.Commentary,
                            Section = comment.Section
                        });
                    }
                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogFatal("Error al obtener los comentarios", ex);
                comments = null;
            }
            return comments;
        }

        /**
        * <summary>
        * Este método obtiene los comentarios añadidos por un personal.
        * </summary>
        * <param name="date">Fecha de la cual recuperara el comentario</param>
        * <param name="comment">Contenido de comentario del cual recuperara el comentario</param>
        * <returns>Retorna una lista de comentarios.</returns>
        */
        public int GetCommentId (DateTime date, string comment) {
            int query = 0;
            try {
                using(var context = new KomalliEntities()) {
                    query = context.Logbook
                        .Where(logbook => logbook.Date == date && logbook.Commentary == comment)
                        .Select(logbook => (int)logbook.IDCommentary)
                        .FirstOrDefault();

                    
                }
            }catch(EntityException ex) {
                LoggerManager.Instance.LogFatal("Error al obtener el id del comentario", ex);
                
            }
            return query;
        }

        /**
        * <summary>
        * Este método obtiene los comentarios añadidos por un personal.
        * </summary>
        * <param name="idcomment">identificador de comentario del cual eliminara el comentario</param>
        * <returns>Retorna una lista de comentarios.</returns>
        */
        public int DeleteCommentary(int idComment) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var comment = context.Logbook.FirstOrDefault(comment => comment.IDCommentary == idComment);
                    context.Logbook.Remove(comment);
                    result = context.SaveChanges();
                }
            } catch (DbUpdateException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error el comentario", ex);
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error el comentario", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método añade los comentarios dentro de la bitácora.
         * </summary>
         * <param name="logbookModel">se pasa el objeto como referencia para la actualizacion dentro de la base</param>
         * <param name="idcomment">identificador de comentario del cual actualizara el comentario</param>
         * <returns>Si la operación se realiza correctamente, retorna 0. Si occurre algún error, retorna -1.</returns>
         */
        public int UpdateComment(LogbookModel logbook, int idComment) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    var comment = context.Logbook.Where(comment => comment.IDCommentary == idComment).FirstOrDefault();
                    if (comment != null) {
                        comment.Section = logbook.Section;
                        comment.Commentary = logbook.Commentary;
                    }
                    result = context.SaveChanges();
                }
            }
            catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error en actualizar el comentario", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de obtener todos los datos de los comentarios registrados en la base de datos.
         * </summary>
         * <returns> Regresa la lista de los ingredientes obtenidos en la base de datos.</returns>
         */
        public List<LogbookModel> GetAllComments() {
            List<LogbookModel> logbook = new List<LogbookModel>();
            logbook = null;
            try {
                using (var context = new KomalliEntities()) {
                    var query = context.Logbook.Select(comment => new {
                        comment.Section,
                        comment.Commentary,
                        comment.Date,
                        comment.NoPersonalEmployee
                    }).ToList()
                    .Select(comment => new LogbookModel {
                        Section = comment.Section,
                        Commentary = comment.Commentary,
                        Date = comment.Date,
                        NoPersonalEmployee = comment.NoPersonalEmployee
                    }).ToList();

                    logbook = query;
                }
            }catch(EntityException ex) {
                logbook = null;
                LoggerManager.Instance.LogError("Error al consultar un comentario", ex);
            }
            return logbook;
        }
    }
}