import React, { useEffect, useRef, useState } from "react";

interface TimerProps {
    seconds: number;
    onExpire?: () => void;
};

const Timer: React.FC<TimerProps> = ({ seconds, onExpire }) => {
    const [secondsLeft, setSecondsLeft] = useState(seconds);
    const intervalRef = useRef<ReturnType<typeof setInterval> | null>(null);

    useEffect(() => {
        // setSecondsLeft(seconds); // reset timer if seconds prop changes
        intervalRef.current = setInterval(() => {
            setSecondsLeft((prev) => {
                if (prev <= 1) {
                    clearInterval(intervalRef.current!);
                    if (onExpire) {
                        onExpire();
                    };
                    return 0;
                }
                return prev - 1;
            });
        }, 1000);

        return () => {
            if (intervalRef.current) {
                clearInterval(intervalRef.current)
            };
        };
    }, [seconds]);

    return (
        <span className="d-block px-1 text-red-500 font-bold">
            {secondsLeft}
        </span>
    );
};

export default Timer;
