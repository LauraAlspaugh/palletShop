export class Listing{
    constructor(data){
        this.id = data.id
        this.name = data.name
        this.description = data.description
        this.price = data.price
        this.img = data.img
        this.category = data.category
        this.creatorId = data.creatorId
        this.creator = data.creator
        this.createdAt = new Date(data.createdAt).toLocaleDateString()
        this.updatedAt = new Date(data.updatedAt).toLocaleDateString()
    }
}