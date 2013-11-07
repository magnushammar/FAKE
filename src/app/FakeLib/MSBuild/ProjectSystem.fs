module Fake.MsBuild.ProjectSystem

open Fake
open System.Xml
open System.Xml.Linq

type ProjectSystem(projectFile : string) = 
    let document = 
        ReadFileAsString projectFile        
        |> XMLHelper.XMLDoc

    let nsmgr = 
        let nsmgr = new XmlNamespaceManager(document.NameTable)
        nsmgr.AddNamespace("default", document.DocumentElement.NamespaceURI)
        nsmgr

    let files = 
        let xpath = "/default:Project/default:ItemGroup/default:Compile/@Include"
        [for node in document.SelectNodes(xpath,nsmgr) -> node.InnerText]

    member x.Files = files