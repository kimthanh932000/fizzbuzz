import { Routes, Route } from 'react-router-dom';
import CreateGamePage from '../pages/CreateGamePage';
import GameListPage from '../pages/GameListPage';
import GameSessionPage from '../pages/GameSessionPage';
import GameScorePage from '../pages/GameScorePage';
import GameRulesPage from '../pages/GameRulesPage';

const AppRoutes = () => {
  return (
    <Routes>
      <Route path="/" element={<GameListPage />} />
      <Route path="/games/create" element={<CreateGamePage />} />
      <Route path="/games/:gameId/rules" element={<GameRulesPage />} />
      <Route path="/games/session/:sessionId" element={<GameSessionPage />} />
      <Route path="/games/session/:sessionId/result" element={<GameScorePage />} />
    </Routes>
  );
};

export default AppRoutes;
