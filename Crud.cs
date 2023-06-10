using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudTemplate
{
    internal class Crud
    {
        public static void CreatController(string Entity, string Poject)
        {
            string content;
            content = "using System;\nusing System.Collections.Generic;\nusing System.Linq;\nusing System.Threading.Tasks;\nusing Microsoft.AspNetCore.Mvc;\nusing "+Poject+".Models.BLL;\nusing "+Poject+".Models.Entities;\nnamespace "+Poject+".Controllers{"
            +"\n[Route(\"api/[controller]\")]"
   +"\n [ApiController]"
  +"\n public class "+Entity+"Controller : Controller"
    +"\n{"
               +"\n// GET: api/<"+Entity+"Controller>"
                +"\n[HttpGet]"
                +"\n public JsonResult Get()"
                         +"\n{"
                             +"\ntry"
                             +"\n{"
                                +"\n List<" + Entity +"> " + Entity.ToLower() +"s = BLL_" + Entity +".GetAll();"
                                 +"\nreturn Json(new { success = true, message = \"" + Entity +"s trouvés\", data = " + Entity.ToLower() +"s });"
                            +"\n }"
                             +"\ncatch (Exception e)"
                            +"\n{"
                                +"\nreturn Json(new { success = false, message = e.Message });"
                             +"\n}"
                         +"\n}"
        +"\n// GET api/<"+Entity+"Controller>/5"
        +"\n[HttpGet(\"{id}\")]"
        +"\npublic JsonResult Get(int id)"
            +"\n{"
               +"\n try"
               +"\n {"
                   +"\n "+Entity+" "+Entity.ToLower()+" = BLL_"+Entity+".Get"+Entity+"(id);"
                   +"\n return Json(new { success = true, message = \""+Entity+" trouvé\", data = "+Entity.ToLower()+" });"
                +"\n}"
               +"\n catch (Exception e)"
               +"\n {"
                    +"\nreturn Json(new { success = false, message = e.Message });"
               + "\n}"
            +"\n}"

            +"\n// POST api/<"+Entity+"Controller>"
        +"\n[HttpPost]"
       + "\npublic JsonResult Post([FromForm] "+Entity+" "+Entity.ToLower()+")"
        +"\n{"
           +"\ntry"
           + "\n{"
                +"\nif("+Entity.ToLower()+".Id == 0)"
                +"\n{"
                   +"\n "+Entity.ToLower()+".Id = BLL_"+Entity+".Add("+Entity.ToLower()+");"
                   +"\n return Json(new { success = true, message = \"Ajouté avec succès\", data = "+Entity.ToLower()+" });"
                +"\n}"
                +"\nelse"
                +"\n{"
                  + "\n BLL_"+Entity+".Update("+Entity.ToLower()+".Id, "+Entity.ToLower()+");"
                   +"\n return Json(new { success = true, message = \"Modifié avec succès\", data="+Entity.ToLower()+" });"
               +"\n }"

            +"\n}"
            +"\ncatch (Exception ex)"
            +"\n{"
                +"\nreturn Json(new { success = false, message = ex.Message });"
            +"\n}"
        +"\n}"
        +"\n// DELETE api/<"+Entity+"Controller>/5"
        +"\n[HttpDelete(\"{id}\")]"
        +"\npublic JsonResult Delete(int id)"
       +"\n {"
           +"\ntry"
            +"\n{"
                +"\nBLL_"+Entity+".Delete(id);"
                +"\nreturn Json(new { success = true, message = \"Supprimé avec succès\" });"
            +"\n}"
            +"\ncatch (Exception ex)"
            +"\n{"
               +"\n return Json(new { success = false, message = ex.Message });"
            +"\n}"
        +"\n}"
    +"\n}"
+"\n}"
;
            using (var fs = File.Create("E:\\"+Entity+"Controller" +".cs"))
            {
                Byte[] text = new UTF8Encoding(true).GetBytes(content);
                fs.Write(text, 0, text.Length);
            }
            
        }
        public static void CreateBllEntity(string Entity, string Project)
        {
            string content = "";
            content = "using " + Project + ".Models.DAL;"
+ "\nusing " + Project + ".Models.Entities;"
+ "\nusing System;"
+ "\nusing System.Collections.Generic;"
+ "\nusing System.Linq;"
+ "\nusing System.Threading.Tasks;"

+ "\nnamespace " + Project + ".Models.BLL"
+ "\n{"
    + "\npublic class BLL_" + Entity + ""
    + "\n{"
       + " \npublic static int Add(" + Entity + " " + Entity.ToLower() + ")"
        + "\n{"
           + "\nreturn DAL_" + Entity + ".Add(" + Entity.ToLower() + ");"
        + "\n}"

       + "\n public static void Update(int id, " + Entity + " " + Entity.ToLower() + ")"
        + "\n{"
           + "\n DAL_" + Entity + ".Update(id, " + Entity.ToLower() + ");"
        + "\n}"

       + "\n public static void Delete(int id)"
        + "\n{"
           + "\n DAL_" + Entity + ".Delete(id);"
        + "\n}"
        + "\npublic static " + Entity + " Get" + Entity + "(int id)"
        + "\n{"
           + "\n return DAL_" + Entity + ".Get"+Entity+"(id);"
        + "\n}"
        + "\npublic static List<" + Entity + "> GetAll()"
        + "\n{"
            + "\nreturn DAL_" + Entity + ".SelectAll();"
        + "\n}"
    + "\n}"
+ "\n}";


            using (var fs = File.Create("E:\\BLL_" + Entity +".cs"))
            {
                Byte[] text = new UTF8Encoding(true).GetBytes(content);
                fs.Write(text, 0, text.Length);
            }
        }

        public static void CreateDalEntity(string Entity, List<string> champs,string Project,List<string> types)
        {
            string content = "";
            string affectationAdd = "";
            string paramettorsTable = "";
            string argumentsTable = "";
            string affectationUpdate = "";
            string affectationGetEntityFromDataRow = "";
            int i = 0;
            foreach (var champ in champs)
            {
                if(champ != "id")
                {
                    if (types[i] != "IFormFile")
                    {
                        paramettorsTable = paramettorsTable + "," + champ;
                    }
                    
                }
                i++;
            }
            i = 0;
            paramettorsTable = paramettorsTable.Substring(1);
            foreach (var champ in champs)
            {
                if(champ != "id")
                {
                    if (types[i] != "IFormFile")
                    {
                        argumentsTable = argumentsTable + ",@" + champ;
                    }
                    
                }
                i++;
            }
            argumentsTable = argumentsTable.Substring(1);
            i = 0;
            foreach (var champ in champs)
            {
                if (champ != "id")
                {
                    if (types[i] == "string" )
                    {
                        affectationAdd = affectationAdd + "\ncommand.Parameters.AddWithValue(\"@" + champ + "\"," + Entity.ToLower() + "." + champ + "?? (object)DBNull.Value);";
                    }
                    else if (types[i] == "int" || types[i] == "float")
                    {
                        affectationAdd = affectationAdd + "\ncommand.Parameters.AddWithValue(\"@" + champ + "\"," + Entity.ToLower() + "." + champ + ");";
                    }


                    i++;
                }
               
            }
            foreach (var champ in champs)
            {
                if(champ != "id")
                {
                    affectationUpdate = affectationUpdate + "," + champ + "=@" + champ;
                }
            }
            affectationUpdate = affectationUpdate.Substring(1);
            i = 0;
            affectationGetEntityFromDataRow = affectationGetEntityFromDataRow + "\n"+Entity.ToLower()+".Id = Convert.ToInt32(dataRow[\"Id\"]);\n";
            foreach (var champ in champs)
            {
                if (types[i] == "string")
                {
                    affectationGetEntityFromDataRow = affectationGetEntityFromDataRow + Entity.ToLower() + "." + champ + " = dataRow[\"" + champ + "\"].ToString();\n";
                }
                else if (types[i] == "int")
                {
                    affectationGetEntityFromDataRow = affectationGetEntityFromDataRow + Entity.ToLower() + "." + champ + " = Convert.ToInt32(dataRow[\"" + champ + "\"]);\n";
                }
                else if(types[i]=="float")
                {
                    affectationGetEntityFromDataRow = affectationGetEntityFromDataRow + Entity.ToLower() + "." + champ + " =Convert.ToSingle( dataRow[\"" + champ + "\"]);\n";
                }
                i++;
                
            }

            content = "\nusing " + Project + ".Models.Entities;"
+ "\nusing " + Project + ".Utilities;"
+ "\nusing System;"
+ "\nusing System.Collections.Generic;"
+ "\nusing System.Data;"
+ "\nusing System.Data.SqlClient;"
+ "\nusing System.Linq;"
+ "\nusing System.Threading.Tasks;"

+ "\nnamespace " + Project + ".Models.DAL"
+ "\n{"
    + "\npublic class DAL_" + Entity + ""
    + "\n{"
       + "\n // Method Add " + Entity + ""
        + "\npublic static int Add(" + Entity + " " + Entity.ToLower() + ")"
        + "\n{"
           + "\n using (SqlConnection con = DBConnection.GetConnection())"
            + "\n{"
               + "\n string StrSQL = \"INSERT INTO "+Entity+" (" + paramettorsTable + ")output INSERTED.Id VALUES (" + argumentsTable + ")\";"
            +"\nSqlCommand command = new SqlCommand(StrSQL, con);"
            + "\n" + affectationAdd
            + "\nreturn Convert.ToInt32(DataBaseAccessUtilities.ScalarRequest(command));"
        + "\n}"
    + "\n}"

    + "\n// Method Update " + Entity + ""
   + "\n public static void Update(int id, " + Entity + " " + Entity.ToLower() + ")"
    + "\n{"
        + "\nusing (SqlConnection con = DBConnection.GetConnection())"
        + "\n{"
           + "\n string StrSQL = \"UPDATE " + Entity + " SET " + affectationUpdate + " WHERE Id = @Id\";"
            + "\nSqlCommand command = new SqlCommand(StrSQL, con);"
            + "\ncommand.Parameters.AddWithValue(\"@Id\", id);"
            + affectationAdd
            + "\nDataBaseAccessUtilities.NonQueryRequest(command);"
        + "\n}"
    + "\n}"

   + "\n // Method Delete " + Entity + ""
    + "\npublic static void Delete(int EntityKey)"
    + "\n{"
        + "\nusing (SqlConnection con = DBConnection.GetConnection())"
        + "\n{"
            + "\nstring StrSQL = \"DELETE FROM " + Entity + " WHERE Id=@EntityKey\";"
            + "\nSqlCommand command = new SqlCommand(StrSQL, con);"
            + "\ncommand.Parameters.AddWithValue(\"@EntityKey\", EntityKey);"
             + "\nDataBaseAccessUtilities.NonQueryRequest(command);"
         + "\n}"
     + "\n}"

    + "\n // Convert Object DataRow in " + Entity + ""
     + "\nprivate static " + Entity + " GetEntityFromDataRow(DataRow dataRow)"
     + "\n{"
        + " \n" + Entity + " " + Entity.ToLower() + " = new " + Entity + "();"
         + affectationGetEntityFromDataRow
         + "\nreturn " + Entity.ToLower() + ";"
     + "\n}"
     + "\n// Fill List Of " + Entity + " With DataTable"
    + "\n private static List<" + Entity + "> GetListFromDataTable(DataTable dt)"
     + "\n{"
        + "\n List<" + Entity + "> list = new List<" + Entity + ">();"
         + "\nif (dt != null)"
         + "\n{"
             + "\nforeach (DataRow dr in dt.Rows)"
                 + "\nlist.Add(GetEntityFromDataRow(dr));"
         + "\n}"
         + "\nreturn list;"
     + "\n}"

     + "\n// Get " + Entity + " By EntityKey"
     + "\npublic static " + Entity + " Get"+Entity+"(int EntityKey)"
     + "\n{"
         + "\nusing (SqlConnection con = DBConnection.GetConnection())"
         + "\n{"
             + "\ncon.Open();"
             + "\nstring StrSQL = \"SELECT * FROM " + Entity + " WHERE Id = @id\";"
            + "\n SqlCommand command = new SqlCommand(StrSQL, con);"
             + "\ncommand.Parameters.AddWithValue(\"@id\", EntityKey);"
              + "\nDataTable dt = DataBaseAccessUtilities.SelectRequest(command);"
              + "\nif (dt != null && dt.Rows.Count != 0)"
                  + "\nreturn GetEntityFromDataRow(dt.Rows[0]);"
              + "\nelse"
                  + "\nreturn null;"
          + "\n}"
      + "\n}"

      + "\n// Get ALL " + Entity + ""
      + "\npublic static List<" + Entity + "> SelectAll()"
      + "\n{"
          + "\nDataTable dataTable;"
          + "\nusing (SqlConnection con = DBConnection.GetConnection())"
          + "\n{"
             + "\n con.Open();"
              + "\nstring StrSQL = \"SELECT * FROM " + Entity +"\"; "
                   + "\nSqlCommand command = new SqlCommand(StrSQL, con);"
                   + "\ndataTable = DataBaseAccessUtilities.SelectRequest(command);"
               + "\n}"
               + "\nreturn GetListFromDataTable(dataTable);"
           + "\n}"
       + "\n}"
   + "\n}"
   ;
            using (var fs = File.Create("E:\\DAL_" + Entity + ".cs"))
            {
                Byte[] text = new UTF8Encoding(true).GetBytes(content);
                fs.Write(text, 0, text.Length);
            }


        }
        public static void CreateEntity(string Entity, List<string> champs, string Project,List<string> types)
        {
            string content = "";
            string attributes = "\npublic int Id { get; set; }";
            string parametors = "";
            string affectation = "";
            int i = 0;
            foreach (var champ in champs)
            {
                attributes = attributes+ "\npublic "+types[i]+" "+champ+" { get; set; }";
                i++;
            }
            i = 0;
            foreach (var champ in champs)
            {
                parametors = parametors + ","+types[i]+" p" + champ ;
                i++;
            }
            parametors = parametors.Substring(1);
            
            foreach (var champ in champs)
            {
                affectation = affectation + "\n" + champ + "= p"+champ+";";
            }
            content = "\nusing System;"
+ "\nusing System.Collections.Generic;"
+ "\nusing System.ComponentModel.DataAnnotations;"
+ "\nusing System.Linq;"
+ "\nusing System.Threading.Tasks;"

+ "\nnamespace " + Project + ".Models.Entities"
+ "\n{"
    + "\npublic class " + Entity + ""
    + "\n{"
    +attributes

        + "\npublic " + Entity + "()"
        + "\n{"
        + "\n}"
        + "\npublic " + Entity + "(" + parametors + ")"
                 + "\n{"
                    + "\n" + affectation
                  + "\n}"
              + "\n}"
          + "\n}";
            using (var fs = File.Create("E:\\" + Entity + ".cs"))
            {
                Byte[] text = new UTF8Encoding(true).GetBytes(content);
                fs.Write(text, 0, text.Length);
            }
        }
    }
}
