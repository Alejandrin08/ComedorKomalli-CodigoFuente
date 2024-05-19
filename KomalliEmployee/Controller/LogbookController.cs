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
         * <param name="idCommentary">id generado para el comentario</param>
         * <param name="date"> Fecha de realización de comentario</param>
         * <param name="commentary">Descripción añadida</param>
         * <param name="noPersonal">número del personal de quién lo realizó</param>
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
    }
}