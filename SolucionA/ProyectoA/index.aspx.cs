using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ProyectoA.Models;

namespace ProyectoA
{
    public partial class index : System.Web.UI.Page
    {


        #region Definición de variables
        Persona objPersona = new Persona();
        Persona objBuscado;        
        static string resulNom;
        static string resulApe1;
        static string resulApe2;
        static string resulID;
        #endregion


        void RegistrarPersona()
        {
            #region CreateDB
            try
            {                
                using (ContextoBDUIA contexto = new ContextoBDUIA())
                {                    
                    objPersona.identificacion = txtIdentifiacion.Text;
                    objPersona.nomPersona = txtNomPersona.Text;
                    objPersona.ape1Persona = txtApe1Persona.Text;
                    objPersona.ape2Persona = txtApe2Persona.Text;                    
                    contexto.Persona.Add(objPersona);
                    contexto.SaveChanges();
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje", "Swal.fire('Se registro con exito')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mensaje", "Swal.fire('" + ex.InnerException.InnerException.Message.Substring(0, 35) + "')", true);

            }
            #endregion
        }



        void EliminarPersona(string pIdentificacion)
        {
            #region DeleteDB
            try
            {
                using (ContextoBDUIA contextoBD = new ContextoBDUIA())
                {                    
                    objBuscado = contextoBD.Persona.Where(c => c.identificacion== pIdentificacion).FirstOrDefault();

                    if (objBuscado != null)
                    {                        
                        contextoBD.Persona.Remove(objBuscado);
                    }                    
                    contextoBD.SaveChanges();
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mesanje", "Swal.fire('Se elimino de forma correcta')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mesanje", "Swal.fire('" + ex.InnerException.InnerException.Message.Substring(0, 35) + "')", true);

            }
            #endregion

        }


        void ActualizarPersona(string pIdentificacion)
        {
            #region UpdateDB
            try
            {
                using (ContextoBDUIA contextoBD = new ContextoBDUIA())
                {                    
                    objBuscado = contextoBD.Persona.Where(c => c.identificacion == pIdentificacion).FirstOrDefault();

                    if (objBuscado != null)
                    {                        
                        objBuscado.nomPersona = txtNomPersona.Text;
                        objBuscado.ape1Persona = txtApe1Persona.Text;
                        objBuscado.ape2Persona = txtApe2Persona.Text;
                    }                    
                    contextoBD.SaveChanges();
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mesanje", "Swal.fire('Se actualizo de forma correcta')", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mesanje", "Swal.fire('" + ex.InnerException.InnerException.Message.Substring(0, 35) + "')", true);

            }
            #endregion

        }

        void ConsultarPersona(string pIdentificacion)
        {
            #region ReadDB
            try
            {
                using (ContextoBDUIA contextoBD = new ContextoBDUIA())
                {                    
                    objBuscado = contextoBD.Persona.Where(c => c.identificacion == pIdentificacion).FirstOrDefault();
                    if (objBuscado != null)
                    {                        
                        resulNom = objBuscado.nomPersona;
                        resulApe1 = objBuscado.ape1Persona;
                        resulApe2 = objBuscado.ape2Persona;
                    }
                    contextoBD.SaveChanges();
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mesanje", "Swal.fire('Se encontro un resultado con el identificador proporcionado')", true);
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Mesanje", "Swal.fire('" + ex.InnerException.InnerException.Message.Substring(0, 35) + "')", true);

            }
            #endregion
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {            
            RegistrarPersona();
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {            
            ActualizarPersona(txtIdentifiacion.Text);
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {            
            EliminarPersona(txtIdentifiacion.Text);
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {            
            resulID = txtIdentifiacion.Text;            
            ConsultarPersona(resulID);            
            lblIdResul.Text = resulID;
            lblNomResul.Text = resulNom;
            lblApe1Resul.Text = resulApe1;
            lblApe2Resul.Text = resulApe2;
        }
    }
}