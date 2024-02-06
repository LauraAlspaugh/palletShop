export class Receipt{
    constructor(data){
        this.id = data.id
        this.createdAt = new Date(data.createdAt).toLocaleDateString()
        this.updatedAt = new Date(data.updatedAt).toLocaleDateString()
        this.purchaseId = data.purchaseId
        this.buyer = data.buyer
        this.street = data.street
        this.city = data.city
        this.state1 = data.state1
        this.zip = data.zip
        this.total = data.total
        this.creator =data.creator
        this.creatorId = data.creatorId
    }
}