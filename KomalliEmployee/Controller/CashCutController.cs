using KomalliEmployee.Contracts;
using KomalliEmployee.Model;
using KomalliEmployee.Model.Utilities;
using KomalliServer;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KomalliEmployee.Controller {
    public class CashCutController : ICashCut {

        /**
         * <summary>
         * Este método se encarga de obtener el último corte de caja
         * </summary>
         * <returns>Regresa el saldo inicial del último corte de caja</returns>
         */
        public int? GetLastInitialBalance() {
            int? result = null;
            try {
                using (var context = new KomalliEntities()) {
                    var lastInitialBalance = context.CashCut
                        .OrderByDescending(cashCut => cashCut.Id)
                        .Select(cashCut => (int?)cashCut.InitialBalance)
                        .ToList()
                        .FirstOrDefault();

                    if (lastInitialBalance != null) {
                        result = lastInitialBalance;
                    } else {
                        result = 0;
                    }

                }
            } catch (EntityException ex) {
                LoggerManager.Instance.LogError("Error al obtener el último corte de caja", ex);
            }
            return result;
        }

        /**
         * <summary>
         * Este método se encarga de registrar el corte de caja
         * </summary>
         * <param name="initialBalance">Saldo inicial</param>
         * <returns>Regresa el número de registros afectados</returns>
         */

        public int RegisterCashCutNextDay(int initialBalance) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    context.CashCut.Add(new CashCut {
                        Date = DateTime.Now,
                        InitialBalance = initialBalance
                    });
                    result = context.SaveChanges();
                }
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al registrar el corte de caja", ex);
            }
            return result;
        }
        /**
         * <summary>
         * Este método se encarga de actualizar el corte de caja
         * </summary>
         * <param name="initialBalance">Saldo inicial</param>
         * <returns>Regresa el número de registros afectados</returns>
         */
        public int UpdateDailyCashCut(CashCutModel cashCutModel) {
            int result = 0;
            try {
                using (var context = new KomalliEntities()) {
                    CashCut cashCut = context.CashCut.OrderByDescending(cashCut => cashCut.Id).FirstOrDefault();
                    if (cashCut != null) {
                        cashCut.Date = DateTime.Now;
                        cashCut.InitialBalance = cashCutModel.InitialBalance;
                        cashCut.TotalEntries = cashCutModel.TotalEntries;
                        cashCut.TotalExits = cashCutModel.TotalExits;
                        cashCut.Balance = cashCutModel.Balance;
                        result = context.SaveChanges();
                    }
                }
            } catch (EntityException ex) {
                result = -1;
                LoggerManager.Instance.LogError("Error al actualizar el corte de caja", ex);
            }
            return result;  
        }
    }
}
