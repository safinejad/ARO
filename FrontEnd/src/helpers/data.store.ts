
const DataMap = new WeakMap<any, any>();



export function getValue<T extends object, K extends keyof T>(obj: T, key: K): T[K] {
  const map = getMap(obj);
  return map && map[key];
}

export function setValue<T extends object, K extends keyof T>(obj: T, key: K, value: T[K]): void {
  const map = getMap(obj, true);
  if (map)
    map[key] = value;
}

export function getOrCreateValue<T extends object, K extends keyof T>(obj: T, key: K, creator: () => T[K]): T[K] {
  const map = getMap(obj, true);
  if (!(key in map)) {
    map[key] = creator();
  }

  return map[key];
}

function getMap(obj: any, create?: boolean): any {
  let map = DataMap.get(obj);
  if (!map && create && obj) {
    DataMap.set(obj, (map = {}));
  }

  return map;
}
