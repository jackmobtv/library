const isSystemThemeSetToDark = window.matchMedia("(prefers-color-scheme: dark)").matches;

if (isSystemThemeSetToDark) {
    document.documentElement.dataset.mdbTheme = "dark";
}

document.addEventListener("DOMContentLoaded", () => {
    const themeStitcher = document.getElementById("themingSwitcher");
    let isSystemThemeSetToDark = window.matchMedia("(prefers-color-scheme: dark)").matches;

    const savedSetting = localStorage.getItem("dark-mode");
    if (savedSetting) {
        isSystemThemeSetToDark = savedSetting === "dark";
    } else {
        localStorage.setItem("dark-mode", isSystemThemeSetToDark ? "dark" : "light");
    }

    if (isSystemThemeSetToDark) {
        themeStitcher.checked = true;
        document.documentElement.dataset.bsTheme = "dark";
    } else {
        themeStitcher.checked = false;
        document.documentElement.dataset.bsTheme = "light";
    }

    const toggleTheme = (isChecked) => {
        const theme = isChecked ? "dark" : "light";
        localStorage.setItem("dark-mode", theme);
        document.documentElement.dataset.bsTheme = theme;
        console.log("Theme toggled to:", theme);
    }

    themeStitcher.addEventListener("change", (event) => {
        toggleTheme(event.target.checked);
        console.log("Toggle Changed:", event.target.checked);
    });
});