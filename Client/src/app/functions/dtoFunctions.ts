export class DtoFunctions {
    private counter: number = 0;

    public generateId() {
        this.counter++;
        return `fake-id-${this.counter}`;
    }
}