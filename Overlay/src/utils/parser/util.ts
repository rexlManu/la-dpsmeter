export function tryParseInt(intString: string, defaultValue = 0, radix = 10, float = false) {
  if (typeof intString === "number") {
    if (isNaN(intString)) return defaultValue;
    return intString;
  }

  let intNum: number;

  try {
    intNum = float ? parseFloat(intString) : parseInt(intString, radix);
    if (isNaN(intNum)) intNum = defaultValue;
  } catch {
    intNum = defaultValue;
  }

  return intNum;
}
