﻿export class SetDescriptionRequest{
    public id: number;
    public description: string;
    
    constructor(id: number, description: string) {
        this.id = id;
        this.description = description;
    }
}