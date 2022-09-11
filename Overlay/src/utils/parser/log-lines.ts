import { tryParseInt } from "./util";

class LogLine {
  lineSplit: string[];
  timestamp: Date;

  constructor(lineSplit: string[]) {
    this.lineSplit = lineSplit;
    this.timestamp = new Date(this.lineSplit[1]);
  }
}

// logId = 0
export class LogMessage extends LogLine {
  message: string;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.message = this.lineSplit[2];
  }
}

// logId = 1
export class LogInitEnv extends LogLine {
  playerId: string;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.playerId = lineSplit[2];
  }
}

// logId = 2
export class LogPhaseTransition extends LogLine {
  phaseCode: number;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.phaseCode = tryParseInt(lineSplit[2]);
  }
}

// logId = 3
export class LogNewPc extends LogLine {
  id: string;
  name: string;
  classId: number;
  class: string;
  gearScore: number;
  currentHp: number;
  maxHp: number;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.id = lineSplit[2];
    this.name = lineSplit[3] || "Unknown Entity";
    this.classId = tryParseInt(lineSplit[4]);
    this.class = lineSplit[5] || "UnknownClass";
    /* this.level = tryParseInt(lineSplit[6]); */
    this.gearScore = tryParseInt(lineSplit[7], 0, 10, true);
    this.currentHp = tryParseInt(lineSplit[8]);
    this.maxHp = tryParseInt(lineSplit[9]);
  }
}

// logId = 4
export class LogNewNpc extends LogLine {
  id: string;
  npcId: number;
  name: string;
  currentHp: number;
  maxHp: number;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.id = lineSplit[2];
    this.npcId = tryParseInt(lineSplit[3]);
    this.name = lineSplit[4] || "Unknown Entity";
    this.currentHp = tryParseInt(lineSplit[5]);
    this.maxHp = tryParseInt(lineSplit[6]);
  }
}

// logId = 5
export class LogDeath extends LogLine {
  id: string;
  name: string;
  killerId: string;
  killerName: string;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.id = lineSplit[2];
    this.name = lineSplit[3] || "Unknown Entity";
    this.killerId = lineSplit[4];
    this.killerName = lineSplit[5] || "Unknown Entity";
  }
}

// logId = 6
export class LogSkillStart extends LogLine {
  id: string;
  name: string;
  skillId: number;
  skillName: string;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.id = lineSplit[2];
    this.name = lineSplit[3] || "Unknown Entity";
    this.skillId = tryParseInt(lineSplit[4]);
    this.skillName = lineSplit[5] || "Unknown Skill";
  }
}

// logId = 7
export class LogSkillStage extends LogLine {
  id: string;
  name: string;
  skillId: string;
  skillName: string;
  skillStage: number;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.id = lineSplit[2];
    this.name = lineSplit[3] || "Unknown Entity";
    this.skillId = lineSplit[4];
    this.skillName = lineSplit[5] || "Unknown Skill";
    this.skillStage = tryParseInt(lineSplit[6]);
  }
}

// logId = 8
export class LogDamage extends LogLine {
  id: string;
  name: string;
  skillId: number;
  skillName: string;
  skillEffectId: number;
  skillEffect: string;
  targetId: string;
  targetName: string;
  damage: number;
  damageModifier: number;
  currentHp: number;
  maxHp: number;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.id = lineSplit[2];
    this.name = lineSplit[3] || "Unknown Entity";
    this.skillId = tryParseInt(lineSplit[4]);
    this.skillName = lineSplit[5] || "Unknown Skill";
    this.skillEffectId = tryParseInt(lineSplit[6]);
    this.skillEffect = lineSplit[7];
    this.targetId = lineSplit[8];
    this.targetName = lineSplit[9] || "Unknown Entity";
    this.damage = tryParseInt(lineSplit[10]);
    this.damageModifier = tryParseInt(lineSplit[11], 0, 16);
    this.currentHp = tryParseInt(lineSplit[12]);
    this.maxHp = tryParseInt(lineSplit[13]);
  }
}

// logId = 9
export class LogHeal extends LogLine {
  id: string;
  name: string;
  healAmount: number;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.id = lineSplit[2];
    this.name = lineSplit[3] || "Unknown Entity";
    this.healAmount = tryParseInt(lineSplit[4]);
    //this.currentHp = tryParseInt(lineSplit[5]);
  }
}

// logId = 10
export class LogBuff extends LogLine {
  id: string;
  name: string;
  buffId: string;
  buffName: string;
  isNew: boolean;
  sourceId: string;
  sourceName: string;
  shieldAmount: number;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.id = lineSplit[2];
    this.name = lineSplit[3] || "Unknown Entity";
    this.buffId = lineSplit[4];
    this.buffName = lineSplit[5];
    this.isNew = lineSplit[6] == "1";
    this.sourceId = lineSplit[7];
    this.sourceName = lineSplit[8] || "Unknown Entity";
    this.shieldAmount = tryParseInt(lineSplit[9]);
  }
}

// logId = 11
export class LogCounterattack extends LogLine {
  id: string;
  name: string;

  constructor(lineSplit: string[]) {
    super(lineSplit);

    this.id = lineSplit[2];
    this.name = lineSplit[3] || "Unknown Entity";
    /* this.targetId = lineSplit[4];
    this.targetName = lineSplit[5] || "Unknown Entity"; */
  }
}
