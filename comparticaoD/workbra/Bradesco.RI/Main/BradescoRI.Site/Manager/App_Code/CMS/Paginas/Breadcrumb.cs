using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Breadcrumb
/// </summary>
public class Breadcrumb
{
    #region Propriedades
    public virtual string Titulo{ get; set; }
    public virtual string Descricao { get; set; }
    public virtual string Breadcrumbs { get; set; }
    #endregion
}