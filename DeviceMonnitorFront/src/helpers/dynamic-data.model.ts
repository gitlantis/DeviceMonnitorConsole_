import { DataParts } from "./child-data.model"

export class DynamicData{
    deviceGuid :string
    name:string
    lastDataTime:Date
    isWorking:boolean
    isActive :boolean    
    rowCount:number
    aI:Array<DataParts>
    dI:Array<DataParts>
    metadata:Array<DataParts>
}