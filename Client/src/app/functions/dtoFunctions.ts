export class DtoFunctions {
    private counter: number = 0;

    public generateId() {
        let uniqueId = Math.random().toString(36).substring(2) + Date.now().toString(36);
        return uniqueId;
    }
}