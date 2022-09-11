<template>
  <div ref="damageMeterRef">
    <nav
      class="nav q-electron-drag"
      :class="
        settingsStore.settings.damageMeter.design.compactDesign &&
        !isMinimized &&
        !isTakingScreenshot
          ? 'compact-nav'
          : ''
      "
    >
      <span
        v-if="!isMinimized"
        :class="
          settingsStore.settings.damageMeter.design.compactDesign &&
          !isTakingScreenshot
            ? 'time-compact'
            : 'time'
        "
      >
        {{ millisToMinutesAndSeconds(fightDuration) }}
      </span>
      <div class="info-box">
        <div
          v-if="
            !settingsStore.settings.damageMeter.design.compactDesign ||
            isMinimized ||
            isTakingScreenshot
          "
        >
          DPS Meter
        </div>
        <div
          v-if="!isMinimized && sessionState.damageStatistics"
          class="q-electron-drag--exception"
        >
          <q-menu touch-position context-menu>
            <q-list dense style="min-width: 100px">
              <q-item
                v-for="tabName in Object.keys(
                  settingsStore.settings.damageMeter.header
                )"
                :key="tabName"
                clickable
                @click="toggleHeaderDisplay(tabName)"
              >
                <q-item-section side>
                  <q-icon
                    v-if="
                      settingsStore.settings.damageMeter.header[tabName].enabled
                    "
                    name="check"
                  />
                  <q-icon v-else name="close" />
                </q-item-section>
                <q-item-section>
                  {{ settingsStore.settings.damageMeter.header[tabName].name }}
                </q-item-section>
              </q-item>
            </q-list>
          </q-menu>
          <span
            v-if="settingsStore.settings.damageMeter.header.damage.enabled"
            style="margin-right: 12px"
          >
            Total DMG
            {{ numberFormat(sessionState.damageStatistics.totalDamageDealt) }}
          </span>
          <span
            v-if="settingsStore.settings.damageMeter.header.dps.enabled"
            style="margin-right: 12px"
          >
            Total DPS
            {{ numberFormat(sessionDPS) }}
          </span>
          <span
            v-if="settingsStore.settings.damageMeter.header.tank.enabled"
            style="margin-right: 12px"
          >
            Total TNK
            {{ numberFormat(sessionState.damageStatistics.totalDamageTaken) }}
          </span>
          <span
            v-if="
              settingsStore.settings.damageMeter.header.health.enabled &&
              sessionBoss &&
              sessionState.damageStatistics &&
              sessionState.damageStatistics?.totalDamageDealt &&
              fightDuration > 0
            "
            style="margin-right: 12px"
          >
            Boss HP
            {{ numberFormat(sessionBoss.currentHp) }} /
            {{ numberFormat(sessionBoss.maxHp) }} ({{
              Math.floor((sessionBoss.currentHp / sessionBoss.maxHp) * 100)
            }}%)
          </span>
        </div>
      </div>
      <div v-if="!isTakingScreenshot" style="margin-left: auto">
        <q-btn
          v-if="!isMinimized"
          round
          icon="screenshot_monitor"
          @click="takeScreenshot"
          flat
          size="sm"
        >
          <q-tooltip> Take a screenshot of the damage meter </q-tooltip>
        </q-btn>
        <!-- <q-btn
          v-if="!isMinimized"
          round
          icon="fa-solid fa-ghost"
          @click="enableClickthrough"
          flat
          size="sm"
        >
          <q-tooltip ref="clickthroughTooltip">
            Enable clickthrough on damage meter
          </q-tooltip>
        </q-btn> -->
        <q-btn
          v-if="!isMinimized"
          round
          :icon="isFightPaused ? 'play_arrow' : 'pause'"
          @click="toggleFightPause"
          flat
          size="sm"
        >
          <q-tooltip> Pause timer </q-tooltip>
        </q-btn>
        <!-- <q-btn
          round
          :icon="isMinimized ? 'add' : 'remove'"
          @click="toggleMinimizedState"
          flat
          size="sm"
        >
          <q-tooltip> Minimize damage meter </q-tooltip>
        </q-btn> -->
      </div>
      <span v-else class="watermark-box">
        <img class="watermark-logo" :src="logoImg" />
        github.com/karaeren/loa-details
      </span>
    </nav>

    <DamageMeterTable
      v-if="!isMinimized && sessionState"
      :session-state="sessionState"
      :duration="fightDuration"
      :damage-type="damageType"
      :wrapper-style="`height:calc(100vh - 32px - ${
        settingsStore?.settings?.damageMeter?.design?.compactDesign
          ? '32'
          : '64'
      }px)`"
      :name-display="
        settingsStore?.settings?.damageMeter?.functionality?.nameDisplayV2
      "
    />

    <footer v-if="!isMinimized" class="footer">
      <div>
        <q-btn flat size="sm" @click="damageType = 'dmg'" label="DMG">
          <q-tooltip> Show damage </q-tooltip>
        </q-btn>
        <q-btn flat size="sm" @click="damageType = 'tank'" label="TANK">
          <q-tooltip> Show damage taken </q-tooltip>
        </q-btn>
        <q-btn flat size="sm" @click="damageType = 'heal'" label="HEAL">
          <q-tooltip> Show healing done </q-tooltip>
        </q-btn>
        <q-btn flat size="sm" @click="damageType = 'shield'" label="SHIELD">
          <q-tooltip> Show shield done </q-tooltip>
        </q-btn>
      </div>

      <div style="margin-left: auto">
        <span
          v-if="
            settingsStore.settings.damageMeter.design.compactDesign &&
            !isTakingScreenshot
          "
        >
          v{{ settingsStore.settings.appVersion }}
        </span>
        <q-btn
          flat
          size="sm"
          :disabled="settingsStore.settings.uploads.uploadKey.length != 32"
          :label="`UPLOADING: ${
            settingsStore.settings.uploads.uploadLogs ? ' ON' : ' OFF'
          }`"
          :color="
            settingsStore.settings.uploads.uploadLogs ? 'positive' : 'negative'
          "
        >
          <q-tooltip>Toggles uploading encounters</q-tooltip>
        </q-btn>
        <q-btn
          flat
          size="sm"
          @click="requestSessionRestart"
          label="RESET SESSION"
        >
          <q-tooltip> Resets the timer and damages </q-tooltip>
        </q-btn>
      </div>
    </footer>
  </div>
</template>

<script setup>
import { onMounted, ref, watch } from "vue";

import {
  numberFormat,
  millisToMinutesAndSeconds,
} from "../utils/number-helpers.js";
import { sleep } from "../utils/sleep.js";
import html2canvas from "html2canvas";
import { Buffer } from "buffer";
import { saveAs } from "file-saver";

import { useSettingsStore } from "../stores/settings.js";

import DamageMeterTable from "../components/damage-meter/DamageMeterTable.vue";
import { LogParser } from "../utils/parser/parser.ts";

const logoImg = new URL(`../assets/images/logo.png`, import.meta.url).href;

const settingsStore = useSettingsStore();

const parser = new LogParser(true);
parser.dontResetOnZoneChange =
  settingsStore.settings.damageMeter.functionality.dontResetOnZoneChange;
parser.resetAfterPhaseTransition =
  settingsStore.settings.damageMeter.functionality.resetAfterPhaseTransition;
// parser.splitOnPhaseTransition = false;
parser.removeOverkillDamage =
  settingsStore.settings.damageMeter.functionality.removeOverkillDamage;
const socket = new WebSocket(
  `ws${window.location.protocol.startsWith("https:") ? "s" : ""}://` +
    window.location.host
);
socket.addEventListener("open", () => {
  socket.send("subscribe:PACKET");
});
socket.addEventListener("message", (event) => {
  const [channelName, content] = event.data.toString().split(":");

  if (channelName == "packet") {
    // base64 decode content
    const packet = Buffer.from(content, "base64").toString("utf8");
    parser.parseLogLine(packet);
  }
});

const isMinimized = ref(false);
const isAutoMinimized = ref(false);
const damageType = ref("dmg");

function toggleMinimizedState() {
  // isMinimized.value = !isMinimized.value;
  // window.messageApi.send("window-to-main", {
  //   message: "toggle-damage-meter-minimized-state",
  //   value: isMinimized.value,
  // });
}

const clickthroughTooltip = ref(null);
function enableClickthrough() {}

function toggleHeaderDisplay(tabName) {
  settingsStore.settings.damageMeter.header[tabName].enabled =
    !settingsStore.settings.damageMeter.header[tabName].enabled;

  settingsStore.saveSettings();
}

const sessionDuration = ref(0);
const fightDuration = ref(0);
const isFightPaused = ref(false);
let fightPausedOn = 0;
let fightPausedForMs = 0;

function toggleFightPause() {
  if (fightDuration.value === 0) return;

  if (!isFightPaused.value) {
    fightPausedOn = +new Date();
    isFightPaused.value = true;
  } else {
    fightPausedForMs += +new Date() - fightPausedOn;
    isFightPaused.value = false;
  }
}

const damageMeterRef = ref(null);
const isTakingScreenshot = ref(false);
async function takeScreenshot() {
  isTakingScreenshot.value = true;
  await sleep(600);

  const screenshot = await html2canvas(damageMeterRef.value, {
    backgroundColor: "#121212",
  });

  screenshot.toBlob(
    (blob) => {
      saveAs(blob, `damage-meter-${+new Date()}.png`);
    },
    "image/png",
    1
  );

  isTakingScreenshot.value = false;
}

function requestSessionRestart() {
  parser.softReset();
}

const sessionState = ref({});
const sessionDPS = ref(0);
const sessionBoss = ref(null);

onMounted(() => {
  settingsStore.initSettings();

  parser.on("state-change", (state) => {
    sessionState.value = state;

    if (
      sessionState.value.damageStatistics?.totalDamageDealt &&
      fightDuration.value > 0
    )
      sessionDPS.value = (
        sessionState.value.damageStatistics.totalDamageDealt /
        (fightDuration.value / 1000)
      ).toFixed(0);

    const mobs = Object.values(state.entities);
    if (mobs.length <= 0) return;
    const boss = mobs.sort((a, b) => b.maxHp - a.maxHp)[0];

    sessionBoss.value = boss;
  });

  parser.on("reset-state", (state) => {
    isFightPaused.value = false;
    fightPausedOn = 0;
    fightPausedForMs = 0;
    damageType.value = "dmg";
    sessionDPS.value = 0;
    sessionBoss.value = null;
  });

  parser.on("message", (value) => {
    if (value === "new-zone") {
      if (!isMinimized.value) {
        //   Notify.create({
        //     progress: true,
        //     timeout: 5000,
        //     message: "Changed zone, resetting session.",
        //     color: "primary",
        //     actions: [
        //       {
        //         label: "Cancel",
        //         color: "dark",
        //         handler: () => {
        //           window.messageApi.send("window-to-main", {
        //             message: "cancel-reset-session",
        //           });
        //           Notify.create({
        //             message:
        //               "Reset cancelled. Session won't reset until you click reset or change zones again.",
        //           });
        //         },
        //       },
        //     ],
        //   });
        // }
      } else if (value.startsWith("phase-transition")) {
        // phase-transition-0: raid over
        // phase-transition-1: boss dead, includes argos phases
        // phase-transition-2: wipe

        if (
          value === "phase-transition-0" ||
          value === "phase-transition-1" ||
          value === "phase-transition-2"
        ) {
          if (
            settingsStore.settings.damageMeter.functionality
              .pauseOnPhaseTransition &&
            !isFightPaused.value
          ) {
            toggleFightPause();

            let pauseReason = "Raid Over";
            if (value === "phase-transition-1") {
              pauseReason = "Boss Dead";
            } else if (value === "phase-transition-2") {
              pauseReason = "Wipe/Phase Clear";
            }

            if (!isMinimized.value) {
              // Notify.create({
              //   message: `Paused the session (${pauseReason}).`,
              //   color: "primary",
              // });
            }
          }
        }
      } else {
        // Notify.create({
        //   message: value,
        // });
      }
    }
  });

  setInterval(() => {
    if (Object.keys(sessionState.value).length <= 0) return;

    const curTime = +new Date();

    sessionDuration.value = curTime - sessionState.value.startedOn;

    if (sessionState.value.fightStartedOn > 0) {
      if (!isFightPaused.value)
        fightDuration.value =
          curTime - sessionState.value.fightStartedOn - fightPausedForMs;
    } else fightDuration.value = 0;
  }, 1000);
});
</script>

<style>
* {
  margin: 0;
  padding: 0;
  touch-action: manipulation;
}
html,
body {
  background-color: rgba(0, 0, 0, 0.3) !important;
  width: 100%;
  height: 100%;
  overflow-x: hidden;
  overflow-y: hidden;
}
html {
  color: #ffffff;
  font-family: "Segoe UI", "sans-serif";
  line-height: 1;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  -webkit-transition: 0.3s;
}
::-webkit-scrollbar,
scrollbar {
  width: 0;
  height: 0;
}
div,
li {
  -ms-user-select: none;
  -moz-user-select: -moz-none;
  -khtml-user-select: none;
  -webkit-user-select: none;
  user-select: none;
  list-style: none;
}
.nav,
.footer {
  display: flex;
  align-items: center;
  background: rgb(22, 22, 22, 0.75);
  color: rgb(189, 189, 189);
  height: 64px;
  font-size: 14px;
  padding: 0 8px;
}
.compact-nav {
  height: 32px;
}
.footer {
  height: 32px !important;
}
.nav .title {
  color: #fff;
}
.footer {
  position: fixed;
  width: 100%;
  bottom: 0;
  left: 0;
}
.compact-nav .time-compact,
.compact-nav .info-box {
  margin-top: 2px;
}
.nav .time-compact {
  font-size: 11px;
  color: #fff;
}
.nav .time {
  font-size: 32px;
  margin-left: 8px;
  color: #fff;
}
.nav .info-box {
  margin-left: 12px;
  font-size: 11px;
}
.watermark-box {
  margin-left: auto;
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  margin-bottom: 4px;
}
.watermark-logo {
  width: 112px;
}
</style>
