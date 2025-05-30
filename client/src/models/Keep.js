import { Profile } from "./Account.js";
import { DataBaseItem } from "./DataBaseItem.js";

export class Keep extends DataBaseItem {


  constructor(data) {
    super(data)
    this.name = data.name
    this.description = data.description
    this.imgUrl = data.img
    this.views = data.views
    this.kept = data.kept
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)



  }
}