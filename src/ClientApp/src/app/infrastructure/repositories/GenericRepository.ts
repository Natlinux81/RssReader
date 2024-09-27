import { IGenericService } from "../../domain/interfaces/IGenericService";

export class GenericRepository<TEntity> implements IGenericService<TEntity> {
  private entities: TEntity[] = [];

  async getByIdAsync(id: number): Promise<TEntity | null> {
    const entity = this.entities.find(e => (e as any).id === id) || null;
    return await Promise.resolve(entity);
  }

  async getAllAsync(): Promise<TEntity[]> {
    return await Promise.resolve(this.entities);
  }

  async addAsync(entity: TEntity): Promise<TEntity> {
    this.entities.push(entity);
    return await Promise.resolve(entity);
  }

  update(entity: TEntity): TEntity {
    const index = this.entities.findIndex(e => (e as any).id === (entity as any).id);
    if (index !== -1) {
      this.entities[index] = entity;
    }
    return entity;
  }

  delete(entity: TEntity): void {
    this.entities = this.entities.filter(e => (e as any).id !== (entity as any).id);
  }
}



