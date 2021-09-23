export class PanelData{        
    deviceGuid:string
    id:number
    active: boolean
    name: string
    disabled: boolean = false
    paramSubDomain:Array<string>
    paramName: Array<string>
    values: Array<any>
}