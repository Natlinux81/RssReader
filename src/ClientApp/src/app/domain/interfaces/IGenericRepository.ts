export interface IGenericRepository<TEntity> {
  getByIdAsync(id: number): Promise<TEntity | null>;
  getAllAsync(): Promise<TEntity[]>;
  addAsync(entity: TEntity): Promise<TEntity>;
  update(entity: TEntity): TEntity;
  delete(entity: TEntity): void;
}
