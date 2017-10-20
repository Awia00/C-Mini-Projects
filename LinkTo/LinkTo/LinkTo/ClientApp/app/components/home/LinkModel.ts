export class LinkModel {
    constructor(name: string, url: string) {
        this.name = name;
        this.outUri = url;
    }

    id: number;
    name: string;
    description: string;
    outUri: string;
    localUri: string;
};