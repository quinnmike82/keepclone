export interface Note{
    id : string 
    userId : string
    title : string
    content : string
    createAt : Date
    updateAt : Date
    timerSet : Date
    filePath : string
    isDeleted : boolean
    colorBG : string
    isDraft : boolean
}